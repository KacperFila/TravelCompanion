using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.GetPostcardsByTravel;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetPostcardsByTravelEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<IReadOnlyList<PostcardDetailsDTO>>
{
    private readonly IPostcardService _postcardService;

    public GetPostcardsByTravelEndpoint(IPostcardService postcardService)
    {
        _postcardService = postcardService;
    }

    [Authorize]
    [HttpGet("{travelId:guid}/Postcards")]
    [SwaggerOperation(
        Summary = "Get Postcards By Travel Id",
        Tags = new[] { TravelsEndpoint.PostcardsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult<IReadOnlyList<PostcardDetailsDTO>>> HandleAsync(Guid travelId, CancellationToken cancellationToken = new CancellationToken())
    {
        var postcards = await _postcardService.GetAllByTravelIdAsync(travelId);
        return Ok(postcards);
    }
}