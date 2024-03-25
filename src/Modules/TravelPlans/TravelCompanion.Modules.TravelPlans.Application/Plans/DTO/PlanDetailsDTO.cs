using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;

public class PlanDetailsDTO
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
    //public IList<PointDTO> PlanPoints { get; set; }
}