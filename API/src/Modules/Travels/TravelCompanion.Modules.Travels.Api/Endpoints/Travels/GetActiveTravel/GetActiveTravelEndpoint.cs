using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetActiveTravel;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetActiveTravel : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<TravelDetailsDto>
{
    private readonly ITravelService _travelService;

    public GetActiveTravel(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpGet("Travel/Active")]
    [SwaggerOperation(
        Summary = "Get Active Travel",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult<TravelDetailsDto>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _travelService.GetActiveAsync();

        return Ok(result);
    }
}