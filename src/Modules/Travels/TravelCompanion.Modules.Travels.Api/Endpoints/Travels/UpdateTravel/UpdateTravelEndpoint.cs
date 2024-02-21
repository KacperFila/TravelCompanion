using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services;
using TravelCompanion.Shared.Infrastructure.Api;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.UpdateTravel;

[Route($"{TravelsEndpoint.BasePath}/Travel")]

internal sealed class UpdateTravelEndpoint : EndpointBaseAsync
    .WithRequest<UpdateTravelRequest>
    .WithActionResult
{
    private readonly ITravelService _travelService;

    public UpdateTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpPut("{travelId:guid}")]
    [SwaggerOperation(
        Summary = "Update Travel By Id",
        Tags = new[] { TravelsEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(UpdateTravelRequest request, CancellationToken cancellationToken = default)
    {
        await _travelService.UpdateAsync(request.TravelId, request.Travel);
        return NoContent();
    }
}

