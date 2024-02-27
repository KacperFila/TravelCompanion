using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.TravelPlans.Application.TravelPlanInvitations.Commands;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.RejectTravelPlanInvitation;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class RejectTravelPlanInvitationEndpoint : EndpointBaseAsync
    .WithRequest<RejectInvitationToTravelPlan>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RejectTravelPlanInvitationEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }


    //TODO add authorization and checks
    [HttpDelete("/invitations")]
    [SwaggerOperation(
        Summary = "Reject Travel Plan Invitation",
        Tags = new[] { TravelPlansEndpoint.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(RejectInvitationToTravelPlan command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}