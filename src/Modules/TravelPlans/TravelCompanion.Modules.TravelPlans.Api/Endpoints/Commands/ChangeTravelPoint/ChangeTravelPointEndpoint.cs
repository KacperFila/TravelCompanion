﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.ChangeTravelPoint;


[Route(TravelPlansEndpoint.BasePath)]
internal sealed class ChangeTravelPointEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPoints.Commands.ChangeTravelPoint>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public ChangeTravelPointEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPut("Point/{pointId:guid}")]
    [SwaggerOperation(
        Summary = "Change Travel Point",
        Tags = new[] { TravelPlansEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Application.TravelPoints.Commands.ChangeTravelPoint command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
;    }
}