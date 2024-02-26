﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.AcceptTravelPoint;

[Route(TravelPlansEndpoint.BasePath)]

internal sealed class AcceptTravelPointEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPlans.Commands.AcceptTravelPoint>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AcceptTravelPointEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Accept Travel Point",
        Tags = new[] { TravelPlansEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Application.TravelPlans.Commands.AcceptTravelPoint command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}