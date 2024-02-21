using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Services;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetAllTravels;

[Route($"{TravelsEndpoint.BasePath}/Travel")]
internal sealed class GetAllTravelsEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<List<TravelDto>>
{
    private readonly ITravelService _travelService;

    public GetAllTravelsEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Travels",
        Tags = new[] { TravelsEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<List<TravelDto>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var travels = await _travelService.GetAllAsync();
        return Ok(travels);
    }
}
