﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.Invitations.InviteToTravelPlan;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class InviteToTravelPlanEndpoint : EndpointBaseAsync
    .WithRequest<Application.Invitations.Commands.InviteToTravelPlan>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public InviteToTravelPlanEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPost("Invitation/{planId:guid}/{inviteeId:guid}")]
    [SwaggerOperation(
        Summary = "Invite User to Plan",
        Tags = new[] { TravelPlansEndpoint.InvitationsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Application.Invitations.Commands.InviteToTravelPlan command, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}