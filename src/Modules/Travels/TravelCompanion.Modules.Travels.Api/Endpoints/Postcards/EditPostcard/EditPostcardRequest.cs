using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.Dto;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.EditPostcard;

internal class EditPostcardRequest
{
    [FromRoute(Name = "postcardId")] public Guid PostcardId { get; set; }
    [FromBody] public PostcardUpsertDTO Postcard { get; set; }
}