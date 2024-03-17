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
        var plan = await _planRepository.GetAsync(command.planId);

        if (plan == null)
        {
            throw new PlanNotFoundException(command.planId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        if (!plan.Participants.Contains(_userId))
        {
            throw new UserNotAllowedToChangePlanException(plan.Id);
        }

        var receipt = Receipt.Create(
            plan.Participants.Select(x => x.Value).ToList(),
            Money.Create(command.amount),
            command.planId, null, command.description);

        plan.AddAdditionalCost(receipt);
        await _planRepository.UpdateAsync(plan);
    }
}