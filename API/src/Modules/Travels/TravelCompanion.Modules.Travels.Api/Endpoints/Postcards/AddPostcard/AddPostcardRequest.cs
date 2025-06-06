﻿using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.DTO;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.AddPostcard;

internal class AddPostcardRequest
{
    [FromRoute(Name = "travelId")] public Guid TravelId { get; set; }
    [FromBody] public PostcardUpsertDto Postcard { get; set; }
}