using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.Dto;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.UpdateTravel;

internal class UpdateTravelRequest
{
    [FromRoute(Name = "travelId")] public Guid TravelId { get; set; }
    [FromBody] public TravelDto Travel { get; set; }
}