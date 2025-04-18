﻿using TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class ChangeTravelPointHandler : ICommandHandler<ChangeTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;
    private readonly ITravelPointUpdateRequestRepository _travelPointUpdateRequestRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;

    public ChangeTravelPointHandler(
        ITravelPointRepository travelPointRepository,
        IContext context,
        IPlanRepository planRepository,
        ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository,
        ITravelPlansRealTimeService travelPlansRealTimeService)
    {
        _travelPointRepository = travelPointRepository;
        _context = context;
        _planRepository = planRepository;
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
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

        if (!(plan.OwnerId == _userId || plan.Participants.Select(x => x.ParticipantId).Contains(_userId)))
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        //if (point.IsAccepted)
        //{
        //    throw new CouldNotModifyAcceptedTravelPointException();
        //}

        var request = TravelPointUpdateRequest.Create(command.pointId, plan.Id, _userId, command.placeName);

        await _travelPointUpdateRequestRepository.AddAsync(request);

        var updateRequests = await _travelPointUpdateRequestRepository.GetUpdateRequestsForPlanAsync(plan.Id);
        updateRequests = updateRequests.Where(x => x.TravelPlanPointId == point.Id).ToList();

        var participants = plan.Participants.Select(x => x.ParticipantId.ToString()).ToList();

        var updateRequestResponse = new UpdateRequestUpdateResponse
        {
            UpdateRequests = updateRequests,
            PointId = point.Id,
        };

        await _travelPlansRealTimeService.SendPlanUpdate(participants, plan);
        await _travelPlansRealTimeService.SendPointUpdateRequestUpdate(participants, updateRequestResponse);
    }
}