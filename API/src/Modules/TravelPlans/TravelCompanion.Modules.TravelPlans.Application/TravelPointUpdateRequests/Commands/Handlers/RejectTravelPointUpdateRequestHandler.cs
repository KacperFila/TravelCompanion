﻿using TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.External;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Commands.Handlers;

public class RejectTravelPointUpdateRequestHandler : ICommandHandler<RejectTravelPointUpdateRequest>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly ITravelPointUpdateRequestRepository _travelPointUpdateRequestRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;
    private readonly Guid _userId;

    public RejectTravelPointUpdateRequestHandler(
        IPlanRepository planRepository,
        ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository,
        ITravelPointRepository travelPointRepository,
        IContext context,
        ITravelPlansRealTimeService travelPlansRealTimeService)
    {
        _planRepository = planRepository;
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
        _travelPointRepository = travelPointRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
    }

    public async Task HandleAsync(RejectTravelPointUpdateRequest command)
    {
        var request = await _travelPointUpdateRequestRepository.GetAsync(command.RequestId);

        if (request is null)
        {
            throw new TravelPointUpdateRequestNotFoundException(command.RequestId);
        }

        var planPointId = request?.TravelPlanPointId;
        var pointExists = await _travelPointRepository.ExistAsync(planPointId);

        if (!pointExists)
        {
            throw new TravelPointNotFoundException(planPointId);
        }

        var plan = await _planRepository.GetByPointIdAsync(planPointId);

        if (plan is null)
        {
            throw new PlanNotFoundException(planPointId);
        }

        if (plan.OwnerId != _userId)
        {
            throw new UserNotOwnerOfPlanException(_userId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }
        await _travelPointUpdateRequestRepository.RemoveAsync(request);

        var updateRequests = await _travelPointUpdateRequestRepository.GetRequestsForPointAsync(planPointId);
        var participants = plan.Participants.Select(x => x.ParticipantId.ToString()).ToList();

        var updateRequestResponse = new UpdateRequestUpdateResponse
        {
            UpdateRequests = updateRequests,
            PointId = planPointId
        };

        await _travelPlansRealTimeService.SendPointUpdateRequestUpdate(participants, updateRequestResponse);
    }
}