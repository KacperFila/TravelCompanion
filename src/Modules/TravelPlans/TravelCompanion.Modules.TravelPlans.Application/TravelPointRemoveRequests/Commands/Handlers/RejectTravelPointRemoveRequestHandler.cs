using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointRemoveRequests.Commands.Handlers;

internal class RejectTravelPointRemoveRequestHandler : ICommandHandler<RejectTravelPointRemoveRequest>
{
    private readonly ITravelPointDomainService _travelPointDomainService;

    public RejectTravelPointRemoveRequestHandler(ITravelPointDomainService travelPointDomainService)
    {
        _travelPointDomainService = travelPointDomainService;
    }

    public async Task HandleAsync(RejectTravelPointRemoveRequest command)
    {
        await _travelPointDomainService.RemoveTravelPointRemoveRequest(command.requestId);
    }
}