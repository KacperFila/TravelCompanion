using System.Numerics;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class ChangeTravelPointHandler : ICommandHandler<ChangeTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;
    private readonly ITravelPointUpdateRequestRepository _travelPointUpdateRequestRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public ChangeTravelPointHandler(ITravelPointRepository travelPointRepository, IContext context, IPlanRepository planRepository, ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository)
    {
        _travelPointRepository = travelPointRepository;
        _context = context;
        _planRepository = planRepository;
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(ChangeTravelPoint command)
    {
        var doesPointExist = await _travelPointRepository.ExistAsync(command.pointId);

        if (!doesPointExist)
        {
            throw new TravelPointNotFoundException(command.pointId);
        }

        var point = await _travelPointRepository.GetAsync(command.pointId);
        var plan = await _planRepository.GetAsync(point.PlanId);

        if (!(plan.OwnerId == _userId || plan.Participants.Contains(_userId)))
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        if (!point.IsAccepted)
        {
            throw new CouldNotModifyNotAcceptedTravelPointException();
        }

        var request = TravelPointUpdateRequest.Create(command.pointId, _userId, command.placeName);

        await _travelPointUpdateRequestRepository.AddAsync(request);
    }
}