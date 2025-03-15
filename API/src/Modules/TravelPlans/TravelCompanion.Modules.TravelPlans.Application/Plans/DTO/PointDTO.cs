namespace TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;

public class PointDTO
{
    public Guid Id { get; set; }
    public string PlaceName { get; set; }
    public decimal TotalCost { get; set; }
    public int TravelPlanOrderNumber { get; set; }
}