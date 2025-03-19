using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointRemoveRequests.Commands.Handlers;

internal class AcceptTravelPointRemoveRequestHandler : ICommandHandler<AcceptTravelPointRemoveRequest>
{
    private readonly ITravelPointRemoveRequestRepository _travelPointRemoveRequestRepository;
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;

    public AcceptTravelPointRemoveRequestHandler(
        ITravelPointRemoveRequestRepository travelPointRemoveRequestRepository,
        ITravelPointRepository travelPointRepository,
        IPlanRepository planRepository)
    {
        _travelPointRemoveRequestRepository = travelPointRemoveRequestRepository;
        _travelPointRepository = travelPointRepository;
        _planRepository = planRepository;
    }

    public async Task HandleAsync(AcceptTravelPointRemoveRequest command)
    {
        var request = await _travelPointRemoveRequestRepository.GetAsync(command.requestId);

        if (request is null)
        {
            throw new TravelPointRemoveRequestNotFoundException(command.requestId);
        }

        var point = await _travelPointRepository.GetAsync(request.TravelPointId);
        var plan = await _planRepository.GetAsync(point.PlanId);

        var pointOrderNumber = point.TravelPlanOrderNumber;
        var pointsToRecalculateOrderNumber = plan
            .TravelPlanPoints
            .Where(x => x.TravelPlanOrderNumber > pointOrderNumber)
            .ToList();

        foreach (var pointToRecalculate in pointsToRecalculateOrderNumber)
        {
            pointToRecalculate.DecreaseTravelPlanOrderNumber();
        }

        await _travelPointRepository.RemoveAsync(point);
        await _travelPointRemoveRequestRepository.RemoveAsync(request);

        await _planRepository.UpdateAsync(plan);
    }
}