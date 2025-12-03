using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetMostFrequentCompanionsEndpoint;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetMostFrequentCompanionsEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<List<CommonTravelCompanionDTO>>
{
    private readonly ITravelService _travelService;

    public GetMostFrequentCompanionsEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpGet("Travel/Companions")]
    [SwaggerOperation(
        Summary = "Get Most Frequent Companions for Finished Travels",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<List<CommonTravelCompanionDTO>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var companions = await _travelService.GetTopFrequentCompanionsAsync();
        return Ok(companions);
    }
}