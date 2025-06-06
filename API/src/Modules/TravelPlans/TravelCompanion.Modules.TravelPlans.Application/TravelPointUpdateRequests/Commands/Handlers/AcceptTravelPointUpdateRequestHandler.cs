﻿using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
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

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Commands.Handlers;

internal class AcceptTravelPointUpdateRequestHandler : ICommandHandler<AcceptTravelPointUpdateRequest>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly ITravelPointUpdateRequestRepository _travelPointUpdateRequestRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;
    private readonly INotificationRealTimeService _notificationService;

    public AcceptTravelPointUpdateRequestHandler(
        ITravelPointRepository travelPointRepository,
        ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository,
        IContext context,
        IPlanRepository planRepository,
        ITravelPlansRealTimeService travelPlansRealTimeService,
        INotificationRealTimeService notificationService)
    {
        _travelPointRepository = travelPointRepository;
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
        _context = context;
        _planRepository = planRepository;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
        _notificationService = notificationService;
    }

    public async Task HandleAsync(AcceptTravelPointUpdateRequest command)
    {
        var request = await _travelPointUpdateRequestRepository.GetAsync(command.RequestId);

        if (request is null)
        {
            throw new TravelPointUpdateRequestNotFoundException(command.RequestId);
        }

        var point = await _travelPointRepository.GetAsync(request.TravelPlanPointId);

        if (point is null)
        {
            throw new TravelPointNotFoundException(request.TravelPlanPointId);
        }

        var plan = await _planRepository.GetAsync(point.PlanId);

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            await _notificationService.SendToAsync(
                _context.Identity.Id,
                NotificationMessage.Create(
                    "Accept request",
                    "Plan is not during planning.",
                    NotificationSeverity.Error));

            throw new PlanNotDuringPlanningException(plan.Id);
        }


        if (plan.OwnerId != _userId)
        {
            await _notificationService.SendToAsync(
                _context.Identity.Id,
                NotificationMessage.Create(
                    "Accept request",
                    "You are not the owner of the plan. You cannot accept this request.",
                    NotificationSeverity.Error)
            );

            throw new UserNotAllowedToChangeTravelPointException();
        }

        point.ChangeTravelPointPlaceName(request.PlaceName);

        await _travelPointRepository.UpdateAsync(point);
        await _travelPointUpdateRequestRepository.RemoveAsync(request);

        var participants = plan.Participants
            .Select(x => x.ParticipantId)
            .ToList();

        var updateRequests = await _travelPointUpdateRequestRepository.GetUpdateRequestsForPointAsync(point.Id);

        var updateRequestResponse = new UpdateRequestUpdateResponse
        {
            UpdateRequests = updateRequests.Select(AsUpdateRequestDto),
            PointId = point.Id
        };

        var planDto = AsPlanWithPointsDto(plan);

        await _travelPlansRealTimeService.SendPlanUpdate(participants, planDto);
        await _travelPlansRealTimeService.SendPointUpdateRequestUpdate(participants, updateRequestResponse);

        await _notificationService.SendToAsync(
            _context.Identity.Id,
            NotificationMessage.Create(
                "Update request",
                "Travel Point updated!",
                NotificationSeverity.Information)
        );
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
}