using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetFinishedTravelsCount;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetFinishedTravelsCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<int>
{
    private readonly ITravelService _travelService;

    public GetFinishedTravelsCount(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpGet("Travel/Count/Finished")]
    [SwaggerOperation(
        Summary = "Get Finished Travels Count for User",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<int>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var travelsCount = await _travelService.GetUserFinishedTravelsCountAsync();
        return Ok(travelsCount);
    }
}