using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Services;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetTravel;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetTravelEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<TravelDto>
{
    private readonly ITravelService _travelService;

    public GetTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpGet("{id:guid}")]
    public override async Task<ActionResult<TravelDto>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _travelService.GetAsync(id);

        return Ok(result);
    }
}