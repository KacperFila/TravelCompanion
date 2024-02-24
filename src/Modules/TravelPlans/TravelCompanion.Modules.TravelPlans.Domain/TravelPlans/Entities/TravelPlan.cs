using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.DomainEvents;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

public sealed class TravelPlan : AggregateRoot
{
    public OwnerId OwnerId { get; private set; }
    public List<ParticipantId>? ParticipantIds { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateOnly? From { get; private set; }
    public DateOnly? To { get; private set; }
    public TravelCost TravelPlanCost { get; private set; }
    public List<TravelPoint> TravelPlanPoints { get; private set; }

    public List<ParticipantId> ParticipantPaidIds { get; private set; }
    public bool AllParticipantsPaid { get; private set; }

    public TravelPlan(AggregateId id, OwnerId ownerId, List<ParticipantId> participantIds, string title, string? description,
        DateOnly? from, DateOnly? to, TravelCost travelPlanCost, List<TravelPoint> travelPlanPoints, int version = 0)
    : this(id, ownerId)
    {
        ParticipantIds = participantIds;
        Title = title;
        Description = description;
        From = from;
        To = to;
        TravelPlanCost = travelPlanCost;
        TravelPlanPoints = travelPlanPoints;
        Version = version;
    }

    public TravelPlan(AggregateId id, OwnerId ownerId)
        => (Id, OwnerId) = (id, ownerId);

    public static TravelPlan Create(AggregateId id, OwnerId ownerId, string title, string? description, DateOnly? from,
        DateOnly? to)
    {
        var travelPlan = new TravelPlan(id, ownerId);
        travelPlan.ChangeTitle(title);
        travelPlan.ChangeDescription(description);
        travelPlan.ChangeFrom(from);
        travelPlan.ChangeTo(to);
        travelPlan.ClearEvents();
        travelPlan.TravelPlanCost = new TravelCost();
        travelPlan.TravelPlanPoints = new List<TravelPoint>();
        travelPlan.AllParticipantsPaid = false;
        travelPlan.ParticipantPaidIds = new List<ParticipantId>();
        travelPlan.Version = 0;

        return travelPlan;
    }

    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new EmptyTravelPlanTitleException(Id);
        }

        Title = title; 
        IncrementVersion();
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new EmptyTravelPlanDescriptionException(Id);
        }

        Description = description;
        IncrementVersion();
    }

    public void ChangeFrom(DateOnly? from)
    {
        if (To.HasValue && from > To.Value)
        {
            throw new InvalidTravelPlanDateException(Id);
        }

        if (from < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidTravelPlanDateException(Id);
        }

        From = from;
        IncrementVersion();
    }

    public void ChangeTo(DateOnly? to)
    {
        if (From.HasValue && To < From.Value)
        {
            throw new InvalidTravelPlanDateException(Id);
        }

        if (to < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidTravelPlanDateException(Id);
        }

        To = to;
        IncrementVersion();
    }

    public void AddTravelPoint(TravelPoint travelPoint)
    {
        if (travelPoint is null || string.IsNullOrEmpty(travelPoint.PlaceName))
        {
            throw new InvalidTravelPointException();
        }

        TravelPlanPoints.Add(travelPoint);
        AddEvent(new TravelPlanTravelPointAdded(travelPoint));
    }

    //TODO add methods for TravelCost and TravelPoint
}
