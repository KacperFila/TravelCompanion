using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.InviteToTravelPlan;

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
    [HttpPost("Invitations")]
    [SwaggerOperation(
        Summary = "Invite User to Plan",
        Tags = new[] { TravelPlansEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Application.Invitations.Commands.InviteToTravelPlan command, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}