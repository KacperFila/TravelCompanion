namespace TravelCompanion.Modules.Travels.Core.DTO;

public class TravelPointDto
{
    public Guid Id { get; set; }
    public string PlaceName { get; set; }
    public decimal TotalCost { get; set; }
    public bool IsVisited { get; set; }
}