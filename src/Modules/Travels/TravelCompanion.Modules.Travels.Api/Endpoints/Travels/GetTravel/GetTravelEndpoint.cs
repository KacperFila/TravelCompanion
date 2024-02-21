using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Services;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetTravel;

[Route($"{TravelsEndpoint.BasePath}/Travel")]
internal sealed class GetTravelEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<TravelDto>
{
    private readonly ITravelService _travelService;

    public GetTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpGet("{travelId:guid}")]
    [SwaggerOperation(
        Summary = "Get Travel By Id",
        Tags = new[] { TravelsEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult<TravelDto>> HandleAsync(Guid travelId, CancellationToken cancellationToken = default)
    {
        var result = await _travelService.GetAsync(travelId);

        return Ok(result);
    }
}