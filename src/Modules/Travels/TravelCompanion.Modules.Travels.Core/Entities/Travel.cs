namespace TravelCompanion.Modules.Travels.Core.Entities;

public class Travel
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public IList<Guid>? ParticipantIds { get; set; }
    //public IList<TravelPoint>? TravelPoints { get; set; }
    //public TravelCost TravelCost { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
    public bool allParticipantsPaid { get; set; }
    public bool isFinished { get; set; }
    public float? Rating { get; set; }
}