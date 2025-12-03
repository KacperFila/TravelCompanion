using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetUserTravelsCount;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetUserTravelsCountEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<int>
{
    private readonly ITravelService _travelService;

    public GetUserTravelsCountEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpGet("Travel/Count")]
    [SwaggerOperation(
        Summary = "Get Travels Count for User",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<int>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var travelsCount = await _travelService.GetUserTravelsCountAsync();
        return Ok(travelsCount);
    }
}