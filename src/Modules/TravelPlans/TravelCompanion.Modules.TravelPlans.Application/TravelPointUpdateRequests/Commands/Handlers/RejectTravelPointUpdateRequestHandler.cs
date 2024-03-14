using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Commands.Handlers;

public class RejectTravelPointUpdateRequestHandler
{
    private readonly ITravelPointDomainService _travelPointDomainService;

    public RejectTravelPointUpdateRequestHandler(ITravelPointDomainService travelPointDomainService)
    {
        _travelPointDomainService = travelPointDomainService;
    }

    public async Task HandleAsync(RejectTravelPointUpdateRequest command)
    {
        await _travelPointDomainService.RemoveTravelPointUpdateRequest(command.requestId);
    }
}