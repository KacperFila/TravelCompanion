using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Events.External.Handlers;

public class PointReceiptAddedHandler : IEventHandler<PointReceiptAdded>
{
    private readonly IPlanRepository _planRepository;

    public PointReceiptAddedHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task HandleAsync(PointReceiptAdded @event)
    {
        var plan = await _planRepository.GetAsync(@event.planId);

        if (plan == null)
        {
            throw new PlanNotFoundException(@event.planId);
        }

        plan.CalculateTotalCost();
        await _planRepository.UpdateAsync(plan);
    }
}