using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.RejectTravelPointUpdateRequest;

[Route(TravelPlansModule.BasePath)]
internal sealed class RejectTravelPointUpdateRequestEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPointRemoveRequests.Commands.RejectTravelPointRemoveRequest>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RejectTravelPointUpdateRequestEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpDelete("Point/Update/Rejection/{requestId:guid}")]
    [SwaggerOperation(
        Summary = "Reject Travel Point Update Request",
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