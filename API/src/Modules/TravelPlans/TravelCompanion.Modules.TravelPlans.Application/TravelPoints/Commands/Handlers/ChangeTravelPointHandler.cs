using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class ChangeTravelPointHandler : ICommandHandler<ChangeTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;
    private readonly ITravelPointUpdateRequestRepository _travelPointUpdateRequestRepository;
    private readonly INotificationRealTimeService _notificationService;
    private readonly IContext _context;
    private readonly Guid _userId;

    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;

    public ChangeTravelPointHandler(
        ITravelPointRepository travelPointRepository,
        IContext context,
        IPlanRepository planRepository,
        ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository,
        ITravelPlansRealTimeService travelPlansRealTimeService,
        INotificationRealTimeService notificationService)
    {
        _travelPointRepository = travelPointRepository;
        _context = context;
        _planRepository = planRepository;
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
        _notificationService = notificationService;
    }

    public async Task HandleAsync(ChangeTravelPoint command)
    {
        var doesPointExist = await _travelPointRepository.ExistAsync(command.PointId);

        if (!doesPointExist)
        {
            throw new TravelPointNotFoundException(command.PointId);
        }

        var point = await _travelPointRepository.GetAsync(command.PointId);
        var plan = await _planRepository.GetAsync(point.PlanId);

        if (!DoesUserParticipateInPlan(plan))
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }
        
        var request = TravelPointUpdateRequest.Create(command.PointId, plan.Id, _userId, command.PlaceName);

        await _travelPointUpdateRequestRepository.AddAsync(request);

        var updateRequests = await _travelPointUpdateRequestRepository.GetUpdateRequestsForPointAsync(point.Id);
        updateRequests = updateRequests
            .Where(x => x.TravelPlanPointId == command.PointId)
            .ToList();

        var participants = plan.Participants
            .Select(x => x.ParticipantId)
            .ToList();

        var updateRequestResponse = new UpdateRequestUpdateResponse
        {
            UpdateRequests = updateRequests.Select(AsUpdateRequestDto),
            PointId = point.Id,
        };

        var planDto = AsPlanWithPointsDto(plan);

        await _travelPlansRealTimeService.SendPlanUpdate(participants, planDto);
        await _travelPlansRealTimeService.SendPointUpdateRequestUpdate(participants, updateRequestResponse);

        await _notificationService.SendToAsync(
                _context.Identity.Id,
                NotificationMessage.Create(
                    "Travel point",
                    "Update request created!",
                    _context.Identity.Email,
                    NotificationSeverity.Information));
    }

    private static PlanWithPointsDto AsPlanWithPointsDto(Plan plan)
    {
        return new PlanWithPointsDto()
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

    private static PointDto AsPointDto(TravelPoint point)
    {
        return new PointDto()
        {
            Id = point.Id,
            PlaceName = point.PlaceName,
            TotalCost = point.TotalCost.Amount,
            TravelPlanOrderNumber = point.TravelPlanOrderNumber
        };
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

    private bool DoesUserParticipateInPlan(Plan plan)
    {
        return plan.OwnerId == _userId || plan.Participants.Any(x => x.ParticipantId == _userId);
    }
}