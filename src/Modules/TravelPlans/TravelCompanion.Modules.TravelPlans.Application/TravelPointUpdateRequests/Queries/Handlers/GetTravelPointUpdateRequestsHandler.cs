using TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Queries.Handlers;

public class GetTravelPointUpdateRequestsHandler : IQueryHandler<GetTravelPointUpdateRequest, List<UpdateRequestDto>>
{
    private readonly ITravelPointUpdateRequestRepository _travelPointUpdateRequestRepository;

    public GetTravelPointUpdateRequestsHandler(ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository)
    {
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
    }

    public async Task<List<UpdateRequestDto>> HandleAsync(GetTravelPointUpdateRequest request)
    {
        var updateRequests = await _travelPointUpdateRequestRepository.GetUpdateRequestsForPointAsync(request.PointId);

        return updateRequests
            .Select(AsUpdateRequestDto)
            .ToList();
    }
    private static UpdateRequestDto AsUpdateRequestDto(TravelPointUpdateRequest request)
    {
        return new UpdateRequestDto()
        {
            RequestId = request.RequestId,
            PlanId = request.TravelPlanPointId,
            TravelPlanPointId = request.TravelPlanPointId,
            SuggestedById = request.SuggestedById,
            PlaceName = request.PlaceName,
            CreatedOnUtc = request.CreatedOnUtc,
            ModifiedOnUtc = request.ModifiedOnUtc,
        };
    }
}