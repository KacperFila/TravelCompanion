using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.Services;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.UpdateTravel;

[Route(TravelsEndpoint.BasePath)]

internal sealed class UpdateTravelEndpoint : EndpointBaseAsync
    .WithRequest<UpdateTravelRequest>
    .WithActionResult
{
    private readonly ITravelService _travelService;

    public UpdateTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpPut]
    public override async Task<ActionResult> HandleAsync(UpdateTravelRequest request, CancellationToken cancellationToken = default)
    {
        await _travelService.UpdateAsync(request.TravelId, request.Travel);
        return NoContent();
    }
}

