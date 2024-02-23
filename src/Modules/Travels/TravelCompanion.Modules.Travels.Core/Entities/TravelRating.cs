namespace TravelCompanion.Modules.Travels.Core.Entities;

public class TravelRating
{
    public Guid Id { get; set; }
    public Guid TravelId { get; set; }
    public Guid AddedBy { get; set; }
    public float Value { get; set; }
}