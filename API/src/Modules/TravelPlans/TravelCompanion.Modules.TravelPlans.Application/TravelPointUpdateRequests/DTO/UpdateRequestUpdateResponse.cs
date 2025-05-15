namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;

internal sealed class UpdateRequestUpdateResponse
{
    public IEnumerable<UpdateRequestDTO> UpdateRequests { get; set; } = default!;
    public Guid PointId { get; set; }
}
