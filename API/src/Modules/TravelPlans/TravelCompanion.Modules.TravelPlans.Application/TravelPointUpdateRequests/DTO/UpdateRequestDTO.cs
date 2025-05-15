namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;

public sealed class UpdateRequestDTO
{
    public Guid RequestId { get; set; }
    public Guid PlanId { get; set; }
    public Guid TravelPlanPointId { get; set; }
    public Guid SuggestedById { get; set; }
    public string PlaceName { get; set; } = default!;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
