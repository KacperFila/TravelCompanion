namespace TravelCompanion.Modules.Travels.Shared.DTO;

public class TravelDto
{
    public Guid TravelId { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
    public decimal TotalCostValue { get; set; }
    public decimal TravelAdditionalCostValue { get; set; }
    public decimal PointsAdditionalCostValue { get; set; }
    public List<ReceiptDto> ParticipantsCosts { get; set; }
}