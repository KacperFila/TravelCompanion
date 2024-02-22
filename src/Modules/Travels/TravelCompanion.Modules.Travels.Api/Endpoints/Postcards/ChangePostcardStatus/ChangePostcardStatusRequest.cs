using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.Entities.Enums;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.ChangePostcardStatus;

internal class ChangePostcardStatusRequest
{
    [FromRoute(Name="postcardId")] public Guid PostcardId { get; set; }
    [FromBody] public PostcardStatus PostcardStatus { get; set; }
}