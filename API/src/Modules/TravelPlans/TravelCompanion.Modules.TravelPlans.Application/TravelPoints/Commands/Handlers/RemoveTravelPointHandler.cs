using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class RemoveTravelPointHandler : ICommandHandler<RemoveTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;
    private readonly ITravelPointRemoveRequestRepository _travelPointRemoveRequestRepository;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;
    private readonly IContext _context;
    private readonly Guid _userId;

    public RemoveTravelPointHandler(
        ITravelPointRepository travelPointRepository,
        IPlanRepository planRepository,
        IContext context,
        ITravelPointRemoveRequestRepository travelPointRemoveRequestRepository,
        ITravelPlansRealTimeService travelPlansRealTimeService)
    {
        _travelPointRepository = travelPointRepository;
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _travelPointRemoveRequestRepository = travelPointRemoveRequestRepository;
        _travelPlansRealTimeService = travelPlansRealTimeService;
    }

    public async Task HandleAsync(RemoveTravelPoint command)
    {
        var point = await _travelPointRepository.GetAsync(command.travelPointId);

        if (point is null)
        {
            throw new TravelPointNotFoundException(command.travelPointId);
        }

        var plan = await _planRepository.GetAsync(point.PlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(command.travelPointId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        if (!plan.Participants.Any(x => x.ParticipantId == _userId))
        {
            throw new UserDoesNotParticipateInPlanException(_userId, plan.Id);
        }

        if (plan.OwnerId == _userId)
        {
            plan.RemoveTravelPoint(point);
            await _travelPointRepository.RemoveAsync(point);

            var pointOrderNumber = point.TravelPlanOrderNumber;
            var pointsToRecalculateOrderNumber = plan
                .TravelPlanPoints
                .Where(x => x.TravelPlanOrderNumber > pointOrderNumber)
            .ToList();

            foreach (var pointToRecalculate in pointsToRecalculateOrderNumber)
            {
                pointToRecalculate.DecreaseTravelPlanOrderNumber();
            }

            var participants = plan.Participants.Select(x => x.ParticipantId.ToString()).ToList();
                
            await _planRepository.UpdateAsync(plan);
            await _travelPlansRealTimeService.SendPlanUpdate(participants, plan);
        }
        else
        {
            var removeRequest = TravelPointRemoveRequest.Create(point.Id, _userId);
            await _travelPointRemoveRequestRepository.AddAsync(removeRequest);
        }
    }
}