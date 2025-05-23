using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.ChangeActiveTravel;

[Route(TravelsEndpoint.BasePath)]
internal sealed class ChangeActiveTravelEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly ITravelService _travelService;

    public ChangeActiveTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpPost("Travel/Active")]
    [SwaggerOperation(
        Summary = "Change Active  Travel",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Guid travelId, CancellationToken cancellationToken = default)
    {
        await _travelService.ChangeActiveTravelAsync(travelId);
        return Ok();
    }
}