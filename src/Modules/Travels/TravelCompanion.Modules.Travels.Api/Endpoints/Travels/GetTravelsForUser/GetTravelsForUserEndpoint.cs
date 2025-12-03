using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetTravelsForUser;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetTravelsForUserEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<List<TravelDetailsDto>>
{
    private readonly ITravelService _travelService;

    public GetTravelsForUserEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpGet("Travel/Browse")]
    [SwaggerOperation(
        Summary = "Get All Travels For User",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<List<TravelDetailsDto>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var travels = await _travelService.GetUserTravelsAsync();
        return Ok(travels);
    }
}
