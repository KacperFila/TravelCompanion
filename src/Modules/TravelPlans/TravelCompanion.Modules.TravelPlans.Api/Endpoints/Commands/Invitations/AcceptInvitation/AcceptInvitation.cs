using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.Invitations.AcceptInvitation;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class AcceptInvitation : EndpointBaseAsync
    .WithRequest<Application.Invitations.Commands.AcceptInvitation>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AcceptInvitation(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPut("Invitation/Acceptance/{invitationId:guid}")]
    [SwaggerOperation(
        Summary = "Accept Travel Plan Invitation",
        Tags = new[] { TravelPlansEndpoint.InvitationsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Application.Invitations.Commands.AcceptInvitation command,
        CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}