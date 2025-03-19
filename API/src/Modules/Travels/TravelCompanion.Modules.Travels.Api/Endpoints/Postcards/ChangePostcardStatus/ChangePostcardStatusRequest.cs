using Microsoft.AspNetCore.Mvc;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.ChangePostcardStatus;

internal class ChangePostcardStatusRequest
{
    [FromRoute(Name = "postcardId")] public Guid PostcardId { get; set; }
    [FromBody] public string PostcardStatus { get; set; }
}