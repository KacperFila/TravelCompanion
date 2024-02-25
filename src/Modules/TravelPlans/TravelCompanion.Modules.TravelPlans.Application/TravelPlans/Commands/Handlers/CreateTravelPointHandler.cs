using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlans.Commands.Handlers;

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