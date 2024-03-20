using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Events.External.Handlers;

public sealed class ParticipantAddedToPlanHandler : IEventHandler<ParticipantAddedToPlan>
{
    private readonly IPlanRepository _planRepository;

    public ParticipantAddedToPlanHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task HandleAsync(ParticipantAddedToPlan @event)
    {
        var plan = await _planRepository.GetAsync(@event.planId);

        if (plan == null)
        {
            throw new PlanNotFoundException(@event.planId);
        }

        var additionalCosts = plan.AdditionalCosts;

        foreach (var planAdditionalCost in plan.AdditionalCosts)
        {
            planAdditionalCost.AddReceiptParticipant(@event.participantId);
        }

        await _planRepository.UpdateAsync(plan);
    }
}