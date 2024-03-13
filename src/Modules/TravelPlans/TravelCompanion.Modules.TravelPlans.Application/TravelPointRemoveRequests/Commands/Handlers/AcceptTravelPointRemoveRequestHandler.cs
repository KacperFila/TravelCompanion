using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointRemoveRequests.Commands.Handlers;

public class AcceptTravelPointRemoveRequestHandler : ICommandHandler<AcceptTravelPointRemoveRequest>
{
    private readonly ITravelPointRemoveRequestRepository _travelPointRemoveRequestRepository;
    private readonly ITravelPointRepository _travelPointRepository;
    public AcceptTravelPointRemoveRequestHandler(ITravelPointRemoveRequestRepository travelPointRemoveRequestRepository, ITravelPointRepository travelPointRepository)
    {
        _travelPointRemoveRequestRepository = travelPointRemoveRequestRepository;
        _travelPointRepository = travelPointRepository;
    }

    public async Task HandleAsync(AcceptTravelPointRemoveRequest command)
    {
        var request = await _travelPointRemoveRequestRepository.GetAsync(command.requestId);

        if (request is null)
        {
            throw new TravelPointRemoveRequestNotFoundException(command.requestId);
        }

        var point = await _travelPointRepository.GetAsync(request.TravelPointId);

        await _travelPointRepository.RemoveAsync(point);
        await _travelPointRemoveRequestRepository.RemoveAsync(request);
    }
}