namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;

internal sealed class UpdateRequestUpdateResponse
{
    public IEnumerable<UpdateRequestDto> UpdateRequests { get; set; } = default!;
    public Guid PointId { get; set; }
}
