using TravelCompanion.Shared.Abstractions.Kernel;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public class TravelRating : IAuditable
{
    public Guid Id { get; set; }
    public Guid TravelId { get; set; }
    public Guid AddedBy { get; set; }
    public float Value { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}