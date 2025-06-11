using Microsoft.AspNetCore.Mvc;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.AddPointReceipt;

internal class AddPointReceiptRequest
{
    [FromRoute(Name = "travelPointId")]
    public Guid TravelPointId { get; set; }

    public List<Guid> ParticipantsIds { get; set; } = [];

    public decimal Amount { get; set; }

    public string? Description { get; set; }
}