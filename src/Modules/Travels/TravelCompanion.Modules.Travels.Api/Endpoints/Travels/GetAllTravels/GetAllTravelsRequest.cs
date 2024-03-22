using Microsoft.AspNetCore.Mvc;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetAllTravels;

internal class GetAllTravelsRequest
{
    [FromQuery] public string? searchTerm { get; set; }
}