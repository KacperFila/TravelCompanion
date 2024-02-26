using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlans.Commands.Handlers;

internal sealed class AcceptTravelPointHandler : ICommandHandler<AcceptTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;

    public AcceptTravelPointHandler(ITravelPointRepository travelPointRepository)
    {
        _travelPointRepository = travelPointRepository;
    }

    public async Task HandleAsync(AcceptTravelPoint command)
    {
        var travelPoint = await _travelPointRepository.GetAsync(command.travelPointId);
        travelPoint.AcceptTravelPoint();

        await _travelPointRepository.UpdateAsync(travelPoint);
    }
}