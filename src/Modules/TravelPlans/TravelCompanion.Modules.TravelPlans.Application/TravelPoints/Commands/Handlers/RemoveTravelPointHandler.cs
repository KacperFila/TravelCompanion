using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class RemoveTravelPointHandler : ICommandHandler<RemoveTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlansDomainService _plansDomainService;
    private readonly IContext _context;
    private readonly Guid _userId;

    public RemoveTravelPointHandler(ITravelPointRepository travelPointRepository, IContext context, IPlansDomainService plansDomainService)
    {
        _travelPointRepository = travelPointRepository;
        _context = context;
        _plansDomainService = plansDomainService;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(RemoveTravelPoint command)
    {
        var travelPoint = await _travelPointRepository.GetAsync(command.travelPointId);

        if (travelPoint == null)
        {
            throw new TravelPointNotFoundException(command.travelPointId);
        }

        var planOwnerId = await _plansDomainService.CheckPlanOwnerAsync(travelPoint.PlanId);

        if (planOwnerId != _userId)
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        await _travelPointRepository.RemoveAsync(travelPoint);
        //TODO add event?
    }
}