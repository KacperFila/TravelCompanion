using Microsoft.AspNetCore.Mvc;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.RateTravel;

internal class RateTravelRequest
{
    [FromRoute(Name = "travelId")] public Guid TravelId { get; set; }
    [FromBody] public int Rating { get; set; }
}