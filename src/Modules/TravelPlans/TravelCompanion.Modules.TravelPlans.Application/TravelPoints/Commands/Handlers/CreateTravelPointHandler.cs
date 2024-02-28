using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands;

public sealed class CreateTravelPointHandler : ICommandHandler<CreateTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;

    public CreateTravelPointHandler(ITravelPointRepository travelPointRepository)
    {
        _travelPointRepository = travelPointRepository;
    }

    public async Task HandleAsync(CreateTravelPoint command)
    {
        var travelPoint = TravelPoint.Create(Guid.NewGuid(), command.PlaceName, command.travelPlanId);
        await _travelPointRepository.AddAsync(travelPoint);
    }
}