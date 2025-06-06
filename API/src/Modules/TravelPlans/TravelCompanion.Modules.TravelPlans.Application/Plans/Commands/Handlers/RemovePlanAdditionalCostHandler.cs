﻿using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public sealed class RemovePlanAdditionalCostHandler : ICommandHandler<RemovePlanAdditionalCost>
{
    private readonly IPlanRepository _planRepository;
    private readonly IReceiptRepository _receiptRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public RemovePlanAdditionalCostHandler(IPlanRepository planRepository, IContext context, IReceiptRepository receiptRepository)
    {
        _planRepository = planRepository;
        _context = context;
        _receiptRepository = receiptRepository;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(RemovePlanAdditionalCost command)
    {
        var receipt = await _receiptRepository.GetAsync(command.ReceiptId);

        if (receipt is null)
        {
            throw new ReceiptNotFoundException(command.ReceiptId);
        }

        if (receipt.PlanId is null)
        {
            throw new ReceiptNotAssignedToPlanException(command.ReceiptId);
        }

        var plan = await _planRepository.GetAsync(receipt.PlanId);

        if (plan == null)
        {
            throw new PlanNotFoundException(plan.Id);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        if (!plan.Participants.Any(x => x.ParticipantId == _userId))
        {
            throw new UserNotAllowedToChangePlanException(plan.Id);
        }

        plan.RemoveAdditionalCost(command.ReceiptId);
        await _planRepository.UpdateAsync(plan);
        await _receiptRepository.RemoveAsync(receipt);
    }
}