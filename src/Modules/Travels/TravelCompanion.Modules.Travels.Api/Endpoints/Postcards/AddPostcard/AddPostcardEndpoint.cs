using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Api.Endpoints.Travels;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Modules.Travels.Core.Validators;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.AddPostcard;

[Route($"{TravelsEndpoint.BasePath}/Travels")]
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
    [HttpPost("Postcard")]
    [SwaggerOperation(
        Summary = "Add Postcard",
        Tags = new[] { TravelsEndpoint.PostcardsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(AddPostcardRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        //await _travelDtoValidator.ValidateAndThrowAsync(request, cancellationToken);
        await _postcardService.AddToTravelAsync(request.Postcard, Guid.Parse("06b5004c-a6a3-4210-b301-50facc92d3e7"));
        return Created();
    }
}