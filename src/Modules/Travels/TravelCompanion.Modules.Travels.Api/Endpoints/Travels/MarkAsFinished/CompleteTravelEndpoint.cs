using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.MarkAsFinished;

[Route(TravelsEndpoint.BasePath)]
internal sealed class CompleteTravelEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult
{
    private readonly ITravelService _travelService;

    public CompleteTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpPut("Travel/{travelId:guid}/Complete")]
    [SwaggerOperation(
        Summary = "Complete Travel",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]

    public override async Task<ActionResult> HandleAsync(Guid travelId, CancellationToken cancellationToken = new CancellationToken())
    {
        await _travelService.CompleteTravel(travelId);
        return Ok();
    }
}