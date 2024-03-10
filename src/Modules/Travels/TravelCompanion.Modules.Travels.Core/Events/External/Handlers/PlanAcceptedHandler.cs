using TravelCompanion.Modules.TravelPlans.Shared;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Shared.Abstractions.Events;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;


namespace TravelCompanion.Modules.Travels.Core.Events.External.Handlers;

internal sealed class PlanAcceptedHandler : IEventHandler<PlanAccepted>
{
    private readonly ITravelRepository _travelRepository;
    private readonly ITravelPlansModuleApi _travelPlansModuleApi;

    public PlanAcceptedHandler(ITravelRepository travelRepository, ITravelPlansModuleApi travelPlansModuleApi)
    {
        _travelRepository = travelRepository;
        _travelPlansModuleApi = travelPlansModuleApi;
    }

    public async Task HandleAsync(PlanAccepted @event)
    {
        var travelPlanPoints = await _travelPlansModuleApi.GetPlanTravelPointsAsync(@event.planId);
        var travelPlanReceipts = await _travelPlansModuleApi.GetPlanReceiptsAsync(@event.planId);

        var travelId = Guid.NewGuid();

        var travelPoints = travelPlanPoints.Select(x => AsTravelPoint(travelId, x)).ToList();
        var travelReceipts = AsTravelReceipts(travelId, travelPlanReceipts);

        var travel = new Travel
        {
            Id = travelId,
            OwnerId = @event.ownerId,
            ParticipantIds = @event.participants,
            AdditionalCosts = travelReceipts,
            AdditionalCostsValue = Money.Create(@event.additionalCostsValue),
            Title = @event.title,
            Description = @event.description,
            From = @event.from,
            To = @event.to,
            AllParticipantsPaid = false,
            IsFinished = false,
            Ratings = new List<TravelRating>(),
            RatingValue = null,
            TravelPoints = travelPoints
        };

        await _travelRepository.AddAsync(travel);
    }

    private static TravelPoint AsTravelPoint(Guid travelId, TravelPlans.Domain.Plans.Entities.TravelPoint travelPoint)
    {
        var travelPointId = Guid.NewGuid();

        return new TravelPoint(
            travelPointId,
            travelPoint.PlaceName,
            travelId,
            AsTravelPointReceipts(travelPointId, travelPoint.Receipts),
            travelPoint.TotalCost);
    }

    private static List<Receipt> AsTravelReceipts(Guid travelId, List<TravelPlans.Domain.Plans.Entities.Receipt> receipts)
    {
        var receiptsResult = new List<Receipt>();

        foreach (var receipt in receipts)
        {
            receiptsResult.Add(
                Receipt.Create(
                    travelId,
                    null,
                    receipt.Amount,
                    receipt.Description));
        }

        return receiptsResult;
    }

    private static List<Receipt> AsTravelPointReceipts(Guid pointId, List<TravelPlans.Domain.Plans.Entities.Receipt> receipts)
    {
        var receiptsResult = new List<Receipt>();

        foreach (var receipt in receipts)
        {
            receiptsResult.Add(
                Receipt.Create(
                    null,
                    pointId,
                    receipt.Amount,
                    receipt.Description));
        }

        return receiptsResult;
    }
}