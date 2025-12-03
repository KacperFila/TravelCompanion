using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public sealed class AddPlanAdditionalCostHandler : ICommandHandler<AddPlanAdditionalCost>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public AddPlanAdditionalCostHandler(IPlanRepository planRepository, IContext context)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(AddPlanAdditionalCost command)
    {
        var plan = await _planRepository.GetAsync(command.PlanId);

        if (plan == null)
        {
            throw new PlanNotFoundException(command.PlanId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        if (!plan.Participants.Any(x => x.ParticipantId == _userId))
        {
            throw new UserNotAllowedToChangePlanException(plan.Id);
        }

        var receipt = Receipt.Create(
            _userId,
            plan.Participants.Select(x => x.ParticipantId).ToList(),
            Money.Create(command.Amount),
            command.PlanId,
            null,
            command.Description);

        plan.AddAdditionalCost(receipt);
        await _planRepository.UpdateAsync(plan);
    }
}