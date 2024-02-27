using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.InviteToTravelPlan;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class InviteToTravelPlanEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPlanInvitations.Commands.InviteToTravelPlan>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public InviteToTravelPlanEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("/invitations")]
    [SwaggerOperation(
        Summary = "Invite User to TravelPlan",
        Tags = new[] { TravelPlansEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Application.TravelPlanInvitations.Commands.InviteToTravelPlan command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}