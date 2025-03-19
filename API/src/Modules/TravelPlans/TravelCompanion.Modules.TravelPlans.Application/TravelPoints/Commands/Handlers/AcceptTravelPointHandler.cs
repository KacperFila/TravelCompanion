using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

internal sealed class AcceptTravelPointHandler : ICommandHandler<AcceptTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public AcceptTravelPointHandler(ITravelPointRepository travelPointRepository, IContext context, IPlanRepository planRepository)
    {
        _travelPointRepository = travelPointRepository;
        _context = context;
        _planRepository = planRepository;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(AcceptTravelPoint command)
    {
        var travelPoint = await _travelPointRepository.GetAsync(command.pointId);

        if (travelPoint is null)
        {
            throw new TravelPointNotFoundException(command.pointId);
        }

        var plan = await _planRepository.GetAsync(travelPoint.PlanId);

        if (_userId != plan.OwnerId)
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        travelPoint.AcceptTravelPoint();

        await _travelPointRepository.UpdateAsync(travelPoint);
    }
}