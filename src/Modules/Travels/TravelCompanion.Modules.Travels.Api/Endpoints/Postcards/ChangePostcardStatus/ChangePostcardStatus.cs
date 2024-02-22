using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Api.Endpoints.Travels;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.ChangePostcardStatus;

[Route(TravelsModule.BasePath)]
internal sealed class ChangePostcardStatus : EndpointBaseAsync
    .WithRequest<ChangePostcardStatusRequest>
    .WithActionResult
{
    private readonly IPostcardService _postcardService;

    public ChangePostcardStatus(IPostcardService postcardService)
    {
        _postcardService = postcardService;
    }

    [HttpPut("Postcard/Status/{postcardId:guid}")]
    [SwaggerOperation(
        Summary = "Change Postcard Status By Id",
        Tags = new[] { TravelsEndpoint.PostcardsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(ChangePostcardStatusRequest request, CancellationToken cancellationToken = default)
    {
        await _postcardService.ChangeStatus(request.PostcardId, request.PostcardStatus);
        return NoContent();
    }
}