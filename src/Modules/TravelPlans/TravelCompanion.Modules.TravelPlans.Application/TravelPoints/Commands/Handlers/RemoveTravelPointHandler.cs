using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class RemoveTravelPointHandler : ICommandHandler<RemoveTravelPoint>
{
    private readonly ITravelPointDomainService _travelPointDomainService;

    public RemoveTravelPointHandler(ITravelPointDomainService travelPointDomainService)
    {
        _travelPointDomainService = travelPointDomainService;
    }

    public async Task HandleAsync(RemoveTravelPoint command)
    {
        await _travelPointDomainService.RemoveTravelPoint(command.travelPointId);
    }
}