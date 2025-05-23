using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class RemoveTravelPointHandler : ICommandHandler<RemoveTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;
    private readonly INotificationRealTimeService _notificationService;
    private readonly IContext _context;
    private readonly Guid _userId;

    public RemoveTravelPointHandler(
        ITravelPointRepository travelPointRepository,
        IPlanRepository planRepository,
        IContext context,
        ITravelPlansRealTimeService travelPlansRealTimeService,
        INotificationRealTimeService notificationRealTimeService)
    {
        _travelPointRepository = travelPointRepository;
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
        _notificationService = notificationRealTimeService;
    }

    public async Task HandleAsync(RemoveTravelPoint command)
    {
        var point = await _travelPointRepository.GetAsync(command.TravelPointId);

        if (point is null)
        {
            throw new TravelPointNotFoundException(command.TravelPointId);
        }

        var plan = await _planRepository.GetAsync(point.PlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(command.TravelPointId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        if (!plan.Participants.Any(x => x.ParticipantId == _userId))
        {
            throw new UserDoesNotParticipateInPlanException(_userId, plan.Id);
        }

        if (plan.OwnerId != _userId)
        {
            await _notificationService.SendToAsync(
                _context.Identity.Id,
                NotificationMessage.Create(
                    "Travel point",
                    "You are not allowed to remove this travel point!",
                    _context.Identity.Email,
                    NotificationSeverity.Error));
            throw new UserNotAllowedToChangeTravelPointException();
        }

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

        var participants = plan.Participants
            .Select(x => x.ParticipantId)
            .ToList();
        
        var planDto = AsPlanWithPointsDto(plan);

        await _planRepository.UpdateAsync(plan);

        await _travelPlansRealTimeService.SendPlanUpdate(participants, planDto);
        await _notificationService.SendToGroup(
            participants,
            NotificationMessage.Create(
                "Removed point",
                $"Removed travel point: \"{point.PlaceName}\"!",
                _context.Identity.Email,
                NotificationSeverity.Information));
    }

    private static PlanWithPointsDto AsPlanWithPointsDto(Plan plan)
    {
        return new PlanWithPointsDto
        {
            Id = plan.Id,
            OwnerId = plan.OwnerId,
            Participants = plan.Participants.Select(x => x.ParticipantId).ToList(),
            Title = plan.Title,
            Description = plan.Description,
            From = plan.From,
            To = plan.To,
            AdditionalCostsValue = plan.AdditionalCostsValue.Amount,
            TotalCostValue = plan.TotalCostValue.Amount,
            TravelPlanPoints = plan.TravelPlanPoints.Select(AsPointDto).ToList(),
            PlanStatus = plan.PlanStatus
        };
    }

    private static PointDto AsPointDto(TravelPoint point)
    {
        return new PointDto
        {
            Id = point.Id,
            PlaceName = point.PlaceName,
            TotalCost = point.TotalCost.Amount,
            TravelPlanOrderNumber = point.TravelPlanOrderNumber
        };
    }
}
