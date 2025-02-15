using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.AddPostcard;

[Route(TravelsEndpoint.BasePath)]
internal sealed class AddPostcardEndpoint : EndpointBaseAsync
    .WithRequest<AddPostcardRequest>
    .WithActionResult
{
    private readonly IPostcardService _postcardService;

    public AddPostcardEndpoint(IPostcardService postcardService)
    {
        _postcardService = postcardService;
    }

    [Authorize]
    [HttpPost("Travel/{travelId:guid}/Postcard")]
    [SwaggerOperation(
        Summary = "Add Postcard",
        Tags = new[] { TravelsEndpoint.PostcardsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(AddPostcardRequest request, CancellationToken cancellationToken = default)
    {
        await _postcardService.AddToTravelAsync(request.Postcard, request.TravelId);
        return Created();
    }
}