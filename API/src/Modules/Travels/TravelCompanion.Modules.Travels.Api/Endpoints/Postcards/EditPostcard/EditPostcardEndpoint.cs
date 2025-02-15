using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.EditPostcard;

[Route(TravelsEndpoint.BasePath)]
internal sealed class EditPostcardEndpoint : EndpointBaseAsync
    .WithRequest<EditPostcardRequest>
    .WithActionResult
{
    private readonly IPostcardService _postcardService;

    public EditPostcardEndpoint(IPostcardService postcardService)
    {
        _postcardService = postcardService;
    }

    [Authorize]
    [HttpPut("Travel/Postcard/{postcardId:guid}")]
    [SwaggerOperation(
        Summary = "Edit Postcard",
        Tags = new[] { TravelsEndpoint.PostcardsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(EditPostcardRequest request, CancellationToken cancellationToken = default)
    {
        await _postcardService.UpdateAsync(request.Postcard, request.PostcardId);
        return NoContent();
    }
}