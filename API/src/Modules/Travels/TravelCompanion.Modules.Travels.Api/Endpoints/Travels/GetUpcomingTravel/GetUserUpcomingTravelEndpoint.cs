using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetUpcomingTravel;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetUserUpcomingTravelEndpoint : EndpointBaseAsync
    .WithoutRequest.
    WithActionResult<List<TravelDetailsDto>>
{
    private readonly ITravelService _travelService;

    public GetUserUpcomingTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpGet("Travel/Upcoming")]
    [SwaggerOperation(
        Summary = "Get Upcoming Travel for User",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<List<TravelDetailsDto>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var upcomingTravel = await _travelService.GetUserUpcomingTravelsAsync();
        return Ok(upcomingTravel);
    }
}