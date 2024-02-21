using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.RemoveTravelRating;

[Route($"{TravelsEndpoint.BasePath}/Travel")]
internal sealed class RemoveTravelRating : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly ITravelService _travelService;

    public RemoveTravelRating(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpDelete("{travelId:guid}/Rating/")]
    [SwaggerOperation(
        Summary = "Remove Travel Rating by Travel Id",
        Tags = new[] { TravelsEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Guid travelId, CancellationToken cancellationToken = new CancellationToken())
    {
        await _travelService.RemoveRatingAsync(travelId);
        return NoContent();
    }
}