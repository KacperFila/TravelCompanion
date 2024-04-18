namespace TravelCompanion.Modules.Travels.Shared.DTO;

public class ReceiptDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public List<Guid> ReceiptParticipants { get; set; }
    public Guid? TravelPointId { get; set; }
}