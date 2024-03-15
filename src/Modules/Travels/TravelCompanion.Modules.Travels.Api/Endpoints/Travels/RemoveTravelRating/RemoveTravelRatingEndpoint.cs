using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.RemoveTravelRating;

[Route(TravelsEndpoint.BasePath)]
internal sealed class RemoveTravelRatingEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly ITravelService _travelService;

    public RemoveTravelRatingEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpDelete("Travel/{travelId:guid}/Rating")]
    [SwaggerOperation(
        Summary = "Remove Travel Rating",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Guid travelId, CancellationToken cancellationToken = default)
    {
        await _travelService.RemoveRatingAsync(travelId);
        return NoContent();
    }
}