using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public class Travel : IAuditable
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public List<Guid>? ParticipantIds { get; set; } = new();
    public List<TravelPoint> TravelPoints { get; set; } = new();
    public List<Receipt> AdditionalCosts { get; set; } = new();
    public Money AdditionalCostsValue { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
    public bool AllParticipantsPaid { get; set; }
    public bool IsFinished { get; set; }
    public List<TravelRating> Ratings { get; set; } = new();
    public float? RatingValue { get; set; }
    public Money TotalCostsValue { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}