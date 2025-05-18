using TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Entities;

public sealed class TravelSummary : AggregateRoot, IAuditable
{
    public Guid TravelId { get; private set; }
    public DateOnly? From { get; private set; }
    public DateOnly? To { get; private set; }
    public DateOnly GeneratedOn => DateOnly.FromDateTime(CreatedOnUtc);
    public Money TotalCost { get; private set; }
    public Money TravelAdditionalCost { get; private set; }
    public Money PointsAdditionalCost { get; private set; }
    public List<ParticipantCost> ParticipantsCosts { get; private set; } = new List<ParticipantCost>();
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public TravelSummary(AggregateId id, Guid travelId)
    {
        Id = id;
        TravelId = travelId;
    }

    public static TravelSummary Create(AggregateId id, Guid travelId, DateOnly? from, DateOnly? to,
        Money totalCost, Money travelAdditionalCost, Money pointsAdditionalCost)
    {
        var summary = new TravelSummary(id, travelId);
        summary.ChangeFrom(from);
        summary.ChangeTo(to);
        summary.ChangeTotalCost(totalCost);
        summary.ChangeTravelAdditionalCost(travelAdditionalCost);
        summary.ChangePointsAdditionalCost(pointsAdditionalCost);
        summary.Version = 0;

        return summary;
    }

    public void ChangeFrom(DateOnly? from)
    {
        if (from < DateOnly.FromDateTime(DateTime.UtcNow) || (To != null && from > To))
        {
            throw new InvalidSummaryDateException(Id);
        }

        From = from;
        IncrementVersion();
    }
    public void ChangeTo(DateOnly? to)
    {
        if (to < DateOnly.FromDateTime(DateTime.UtcNow) || (From != default && to < From))
        {
            throw new InvalidSummaryDateException(Id);
        }

        To = to;
        IncrementVersion();
    }
    public void ChangeTotalCost(Money totalCost)
    {
        TotalCost = totalCost;
        IncrementVersion();
    }
    public void ChangeTravelAdditionalCost(Money travelAdditionalCost)
    {
        TravelAdditionalCost = travelAdditionalCost;
        IncrementVersion();
    }
    public void ChangePointsAdditionalCost(Money pointsAdditionalCost)
    {
        PointsAdditionalCost = pointsAdditionalCost;
        IncrementVersion();
    }
    public void AddParticipantsCost(ParticipantCost cost)
    {
        ParticipantsCosts.Add(cost);
        IncrementVersion();
    }
    public void RemoveParticipantsCost(ParticipantCost cost)
    {
        var existingCost = ParticipantsCosts.SingleOrDefault(x => x.Id == cost.Id);

        if (existingCost is null)
        {
            throw new ParticipantCostNotFoundException(cost.Id);
        }

        ParticipantsCosts.Remove(cost);
        IncrementVersion();
    }

}