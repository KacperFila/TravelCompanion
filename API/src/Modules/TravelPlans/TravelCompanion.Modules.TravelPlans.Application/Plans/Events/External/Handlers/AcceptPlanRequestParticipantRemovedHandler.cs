using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Events.External.Handlers;

public class AcceptPlanRequestParticipantRemovedHandler : IEventHandler<AcceptPlanRequestParticipantRemoved>
{
    private readonly IPlanRepository _planRepository;

    public AcceptPlanRequestParticipantRemovedHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task HandleAsync(AcceptPlanRequestParticipantRemoved @event)
    {
        var plan = await _planRepository.GetAsync(@event.planId);

        plan.ChangeStatusToDuringAcceptance();
        await _planRepository.UpdateAsync(plan);
    }
}