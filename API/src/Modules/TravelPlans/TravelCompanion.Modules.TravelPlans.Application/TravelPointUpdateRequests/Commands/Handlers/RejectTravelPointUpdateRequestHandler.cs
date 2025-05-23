using TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.External;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Commands.Handlers;

public class RejectTravelPointUpdateRequestHandler : ICommandHandler<RejectTravelPointUpdateRequest>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly ITravelPointUpdateRequestRepository _travelPointUpdateRequestRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;
    private readonly INotificationRealTimeService _notificationService;
    private readonly Guid _userId;

    public RejectTravelPointUpdateRequestHandler(
        IPlanRepository planRepository,
        ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository,
        ITravelPointRepository travelPointRepository,
        IContext context,
        ITravelPlansRealTimeService travelPlansRealTimeService,
        INotificationRealTimeService notificationService)
    {
        _planRepository = planRepository;
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
        _travelPointRepository = travelPointRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
        _notificationService = notificationService;
    }

    public async Task HandleAsync(RejectTravelPointUpdateRequest command)
    {
        var request = await _travelPointUpdateRequestRepository.GetAsync(command.RequestId);

        if (request is null)
        {
            throw new TravelPointUpdateRequestNotFoundException(command.RequestId);
        }

        var pointId = request?.TravelPlanPointId;
        var pointExists = await _travelPointRepository.ExistAsync(pointId);

        if (!pointExists)
        {
            throw new TravelPointNotFoundException(pointId);
        }

        var plan = await _planRepository.GetByPointIdAsync(pointId);

        if (plan is null)
        {
            throw new PlanNotFoundException(pointId);
        }

        if (plan.OwnerId != _userId)
        {
            await _notificationService.SendToAsync(
                _context.Identity.Id,
                NotificationMessage.Create(
                    "Reject request",
                    "You are not the owner of the plan. You cannot reject this request.",
                    NotificationSeverity.Error));

            throw new UserNotOwnerOfPlanException(_userId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            await _notificationService.SendToAsync(
                _context.Identity.Id,
                NotificationMessage.Create(
                    "Reject request",
                    "Plan is not during planning.",
                    NotificationSeverity.Error));

            throw new PlanNotDuringPlanningException(plan.Id);
        }

        await _travelPointUpdateRequestRepository.RemoveAsync(request);

        var updateRequests = await _travelPointUpdateRequestRepository.GetUpdateRequestsForPointAsync(pointId);
        var participants = plan.Participants
            .Select(x => x.ParticipantId)
            .ToList();

        var updateRequestResponse = new UpdateRequestUpdateResponse
        {
            UpdateRequests = updateRequests.Select(AsUpdateRequestDto),
            PointId = pointId
        };

        await _travelPlansRealTimeService.SendPointUpdateRequestUpdate(participants, updateRequestResponse);
        await _notificationService.SendToAsync(
            _context.Identity.Id,
            NotificationMessage.Create(
                "Update request",
                "Update request removed!",
                NotificationSeverity.Information)
        );
    }

    private static UpdateRequestDTO AsUpdateRequestDto(TravelPointUpdateRequest request)
    {
        return new UpdateRequestDTO()
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
}