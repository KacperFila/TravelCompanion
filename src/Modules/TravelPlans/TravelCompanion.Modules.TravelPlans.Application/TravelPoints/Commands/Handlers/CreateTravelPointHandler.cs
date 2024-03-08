using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public sealed class CreateTravelPointHandler : ICommandHandler<CreateTravelPoint>
{
    private readonly IPlanRepository _planRepository;

    public CreateTravelPointHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }


    public async Task HandleAsync(CreateTravelPoint command)
    {
        var travelPoint = TravelPoint.Create(Guid.NewGuid(), command.PlaceName, command.travelPlanId);

        var plan = await _planRepository.GetAsync(travelPoint.PlanId);
        if (plan is null)
        {
            throw new PlanNotFoundException(travelPoint.PlanId);
        }

        plan.AddTravelPoint(travelPoint);
        await _planRepository.UpdateAsync(plan);
    }
}