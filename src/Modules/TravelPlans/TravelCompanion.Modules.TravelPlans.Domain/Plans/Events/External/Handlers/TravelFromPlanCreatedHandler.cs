using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Events.External.Handlers;

public class TravelFromPlanCreatedHandler : IEventHandler<TravelFromPlanCreated>
{
    private readonly IPlanRepository _planRepository;

    public TravelFromPlanCreatedHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task HandleAsync(TravelFromPlanCreated @event)
    {
        await _planRepository.DeleteAsync(@event.planId);
    }
}