using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class Plan : AggregateRoot
{
    public OwnerId OwnerId { get; private set; }
    public IList<EntityId>? Participants { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateOnly From { get; private set; }
    public DateOnly To { get; private set; }
    public IList<Receipt> AdditionalCosts { get; private set; }
    public Money AdditionalCostsValue { get; private set; }
    public Money TotalCostValue { get; private set; }
    public IList<TravelPoint> TravelPlanPoints { get; private set; }
    public IList<EntityId> ParticipantPaidIds { get; private set; }
    public bool AllParticipantsPaid { get; private set; }

    public Plan(AggregateId id, OwnerId ownerId, string title, string? description, IList<EntityId> participants,
        IList<TravelPoint> travelPlanPoints, DateOnly from, DateOnly to, int version = 0)
        : this(id, ownerId)
    {
        Title = title;
        Description = description;
        Participants = participants;
        TravelPlanPoints = travelPlanPoints;
        From = from;
        To = to;
        AdditionalCosts = new List<Receipt>();
        AdditionalCostsValue = Money.Create(0);
        Version = version;
        TotalCostValue = Money.Create(0);
    }

    public Plan(AggregateId id, OwnerId ownerId)
        => (Id, OwnerId) = (id, ownerId);

    public static Plan Create(AggregateId id, OwnerId ownerId, string title, string? description, DateOnly from,
        DateOnly to)
    {
        var travelPlan = new Plan(id, ownerId);
        travelPlan.ChangeTitle(title);
        travelPlan.ChangeDescription(description);
        travelPlan.ChangeFrom(from);
        travelPlan.ChangeTo(to);
        travelPlan.ClearEvents();
        travelPlan.Participants = new List<EntityId>();
        travelPlan.TravelPlanPoints = new List<TravelPoint>();
        travelPlan.AllParticipantsPaid = false;
        travelPlan.ParticipantPaidIds = new List<EntityId>();
        travelPlan.AdditionalCosts = new List<Receipt>();
        travelPlan.AdditionalCostsValue = Money.Create(0);
        travelPlan.AddParticipant(ownerId);
        travelPlan.CalculateTotalCost();
        travelPlan.Version = 0;

        return travelPlan;
    }

    public void AddAdditionalCost(Receipt receipt)
    {
        AdditionalCosts.Add(receipt);
        CalculateAdditionalCosts();
        CalculateTotalCost();
        IncrementVersion();
    }

    public void RemoveAdditionalCost(Guid receiptId)
    {
        var cost = AdditionalCosts.FirstOrDefault(x => x.Id == receiptId);
        AdditionalCosts.Remove(cost);
        CalculateAdditionalCosts();
        CalculateTotalCost();
        IncrementVersion();
    }

    public void CalculateTotalCost()
    {
        var receipts = TravelPlanPoints.SelectMany(x => x.Receipts).ToList();
        var totalCost = receipts.Sum(x => x.Amount.Amount);
        totalCost += AdditionalCostsValue.Amount;
        TotalCostValue = Money.Create(totalCost);
        IncrementVersion();
    }
    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new EmptyPlanTitleException(Id);
        }

        Title = title; 
        IncrementVersion();
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new EmptyPlanDescriptionException(Id);
        }

        Description = description;
        IncrementVersion();
    }

    public void ChangeFrom(DateOnly from)
    {
        //TODO add validation so from could not be later than to
        if (from < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidPlanDateException(Id);
        }

        From = from;
        IncrementVersion();
    }

    public void ChangeTo(DateOnly to)
    {

        if (to < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidPlanDateException(Id);
        }

        To = to;
        IncrementVersion();
    }

    public void AddParticipant(Guid id)
    {
        Participants.Add(id);
        IncrementVersion();
    }

    public void AddTravelPoint(TravelPoint travelPoint)
    {
        if (travelPoint is null || string.IsNullOrEmpty(travelPoint.PlaceName))
        {
            throw new InvalidTravelPointException();
        }

        TravelPlanPoints.Add(travelPoint);
        IncrementVersion();
    }

    public void RemoveTravelPoint(TravelPoint travelPoint)
    {
        if (travelPoint is null || string.IsNullOrEmpty(travelPoint.PlaceName))
        {
            throw new InvalidTravelPointException();
        }

        TravelPlanPoints.Remove(travelPoint);
        IncrementVersion();
    }

    private void CalculateAdditionalCosts()
    {
        var amountSum = AdditionalCosts.Sum(x => x.Amount.Amount);
        AdditionalCostsValue = Money.Create(amountSum);
    }
}
