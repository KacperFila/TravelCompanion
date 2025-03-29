using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public sealed class CreateTravelPointHandler : ICommandHandler<CreateTravelPoint>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;
    private readonly INotificationRealTimeService _notificationService;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;

    public CreateTravelPointHandler(
        IPlanRepository planRepository,
        IContext context,
        INotificationRealTimeService notificationService,
        ITravelPlansRealTimeService travelPlansRealTimeService)
    {
        _planRepository = planRepository;
        _context = context;
        _notificationService = notificationService;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
    }

    public async Task HandleAsync(CreateTravelPoint command)
    {
        var plan = await _planRepository.GetAsync(command.travelPlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(command.travelPlanId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        var isPointAccepted = _userId == plan.OwnerId;
        var newPointNumber = GetNewTravelPointNumber(plan);

        var point = TravelPoint.Create(Guid.NewGuid(), command.placeName, command.travelPlanId, isPointAccepted, newPointNumber);

        plan.AddTravelPoint(point);
        await _planRepository.UpdateAsync(plan);

        var participants = plan.Participants
                    .Select(x => x.ParticipantId)
                    .Select(x => x.ToString())
                    .ToList();

        var planDto = AsPlanWithPointsDto(plan);

        var notification =
            NotificationMessage.Create(point.PlaceName, "One of your suggested changes has been accepted!", NotificationSeverity.Information);

        await _notificationService.SendToGroup(participants, notification);

        await _travelPlansRealTimeService.SendPlanUpdate(participants, planDto);
    }

    private int GetNewTravelPointNumber(Plan plan)
    {
        return plan.TravelPlanPoints.Count + 1;
    }

    private static PlanWithPointsDTO AsPlanWithPointsDto(Plan plan)
    {
        return new PlanWithPointsDTO()
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
            PlanStatus = plan.PlanStatus,
        };
    }

    private static PointDTO AsPointDto(TravelPoint point)
    {
        return new PointDTO()
        {
            Id = point.Id,
            PlaceName = point.PlaceName,
            TotalCost = point.TotalCost.Amount,
            TravelPlanOrderNumber = point.TravelPlanOrderNumber
        };
    }
}