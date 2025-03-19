using TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Notifications;
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

    public AcceptTravelPointUpdateRequestHandler(
        ITravelPointRepository travelPointRepository,
        ITravelPointUpdateRequestRepository travelPointUpdateRequestRepository,
        IContext context,
        IPlanRepository planRepository,
        ITravelPlansRealTimeService travelPlansRealTimeService)
    {
        _travelPointRepository = travelPointRepository;
        _travelPointUpdateRequestRepository = travelPointUpdateRequestRepository;
        _context = context;
        _planRepository = planRepository;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
    }

    public async Task HandleAsync(AcceptTravelPointUpdateRequest command)
    {
        var request = await _travelPointUpdateRequestRepository.GetAsync(command.RequestId);

        if (request is null)
        {
            throw new TravelPointUpdateRequestNotFoundException(command.RequestId);
        }

        var travelPoint = await _travelPointRepository.GetAsync(request.TravelPlanPointId);

        if (travelPoint is null)
        {
            throw new TravelPointNotFoundException(travelPoint.Id);
        }

        var travelPlan = await _planRepository.GetAsync(travelPoint.PlanId);

        if (travelPlan.OwnerId != _userId)
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        travelPoint.ChangeTravelPointPlaceName(request.PlaceName);

        await _travelPointRepository.UpdateAsync(travelPoint);
        await _travelPointUpdateRequestRepository.RemoveAsync(request);

        var participants = travelPlan.Participants
            .Select(x => x.ParticipantId)
            .Select(x => x.ToString())
            .ToList();

        var updateRequests = await _travelPointUpdateRequestRepository.GetRequestsForPointAsync(travelPoint.Id);

        var updateRequestResponse = new UpdateRequestUpdateResponse
        {
            UpdateRequests = updateRequests,
            PointId = travelPoint.Id
        };

        await _travelPlansRealTimeService.SendPlanUpdate(participants, travelPlan);
        await _travelPlansRealTimeService.SendPointUpdateRequestUpdate(participants, updateRequestResponse);
    }
}