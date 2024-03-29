﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Postcards.RemovePostcard;

[Route(TravelsEndpoint.BasePath)]
internal sealed class RemovePostcardEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly IPostcardService _postcardService;

    public RemovePostcardEndpoint(IPostcardService postcardService)
    {
        _postcardService = postcardService;
    }

    [HttpDelete("Travel/Postcard/{postcardId:guid}")]
    [SwaggerOperation(
        Summary = "Remove Postcard",
        Tags = new[] { TravelsEndpoint.PostcardsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Guid postcardId, CancellationToken cancellationToken = default)
    {
        await _postcardService.DeleteAsync(postcardId);
        return NoContent();
    }
}