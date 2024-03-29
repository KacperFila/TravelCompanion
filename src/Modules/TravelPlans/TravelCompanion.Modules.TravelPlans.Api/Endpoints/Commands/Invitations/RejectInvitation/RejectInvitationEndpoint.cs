﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.Invitations.RejectInvitation;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class RejectInvitationEndpoint : EndpointBaseAsync
    .WithRequest<Application.Invitations.Commands.RejectInvitation>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RejectInvitationEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpDelete("Invitation/{invitationId:guid}/Rejection")]
    [SwaggerOperation(
        Summary = "Reject Travel Plan Invitation",
        Tags = new[] { TravelPlansEndpoint.InvitationsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Application.Invitations.Commands.RejectInvitation command, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}