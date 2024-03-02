using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetAllTravels;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetAllTravelsEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<List<TravelDetailsDTO>>
{
    private readonly ITravelService _travelService;

    public GetAllTravelsEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpGet("Travel")]
    [SwaggerOperation(
        Summary = "Get All Travels",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<List<TravelDetailsDTO>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var travels = await _travelService.GetAllAsync();
        return Ok(travels);
    }
}
