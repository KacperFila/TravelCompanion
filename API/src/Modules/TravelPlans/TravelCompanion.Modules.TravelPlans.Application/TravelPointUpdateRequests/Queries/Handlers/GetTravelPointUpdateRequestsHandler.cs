using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Queries.Handlers;

public class GetTravelPointUpdateRequestsHandler : IQueryHandler<GetTravelPointUpdateRequest, List<TravelPointUpdateRequest>>
{
    private readonly ITravelPointUpdateRequestRepository _travelPointUpdateRequestRepository;

    public GetTravelPointUpdateRequestsHandler(ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository)
    {
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
    }

    public async Task<List<TravelPointUpdateRequest>> HandleAsync(GetTravelPointUpdateRequest request)
    {
        var updateRequests = await _travelPointUpdateRequestRepository.GetUpdateRequestsForPointAsync(request.PointId);

        return updateRequests;
    }
}