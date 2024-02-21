using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.RateTravel;

[Route($"{TravelsEndpoint.BasePath}/Travel")]

internal sealed class RateTravelEndpoint : EndpointBaseAsync
    .WithRequest<RateTravelRequest>
    .WithActionResult
{
    private readonly ITravelService _travelService;

    public RateTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpPut("{travelId:guid}/Rating")]
    [SwaggerOperation(
        Summary = "Rate Travel By Id",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(RateTravelRequest request, CancellationToken cancellationToken = default)
    {
        await _travelService.RateAsync(request.TravelId, request.Rating);
        return NoContent();
    }
}

