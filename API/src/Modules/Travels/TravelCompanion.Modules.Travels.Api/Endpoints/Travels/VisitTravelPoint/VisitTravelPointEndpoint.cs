using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.VisitTravelPoint;

[Route(TravelsEndpoint.BasePath)]
internal sealed class VisitTravelPointEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly ITravelService _travelService;

    public VisitTravelPointEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpPut("Travel/Point/{pointId:guid}/Visitation")]
    [SwaggerOperation(
        Summary = "Visit Travel Point",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Guid pointId, CancellationToken cancellationToken = new CancellationToken())
    {
        await _travelService.VisitTravelPointAsync(pointId);
        return NoContent();
    }
}