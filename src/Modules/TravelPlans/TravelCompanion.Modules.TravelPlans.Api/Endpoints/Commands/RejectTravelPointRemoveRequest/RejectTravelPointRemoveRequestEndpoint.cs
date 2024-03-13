﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.RejectTravelPointRemoveRequest;

[Route(TravelPlansModule.BasePath)]
internal sealed class RejectTravelPointRemoveRequestEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPointRemoveRequests.Commands.RejectTravelPointRemoveRequest>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RejectTravelPointRemoveRequestEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpDelete("Point/Remove/Rejection/{requestId:guid}")]
    [SwaggerOperation(
        Summary = "Reject Travel Point Remove Request",
        Tags = new[] { TravelPlansEndpoint.InvitationsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Application.TravelPointRemoveRequests.Commands.RejectTravelPointRemoveRequest command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}