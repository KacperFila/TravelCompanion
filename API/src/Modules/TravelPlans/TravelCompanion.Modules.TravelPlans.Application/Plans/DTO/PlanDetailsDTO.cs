namespace TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;

public class PlanDetailsDto
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public List<Guid> Participants { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public decimal AdditionalCostsValue { get; set; }
    public decimal TotalCostValue { get; set; }
    public string PlanStatus { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}