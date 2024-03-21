using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.TravelPointUpdateRequests.RejectTravelPointUpdateRequest;

[Route(TravelPlansModule.BasePath)]
internal sealed class RejectTravelPointUpdateRequestEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPointUpdateRequests.Commands.RejectTravelPointUpdateRequest>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RejectTravelPointUpdateRequestEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpDelete("Point/Update/{requestId:guid}/Rejection")]
    [SwaggerOperation(
        Summary = "Reject Travel Point Update Request",
        Tags = new[] { TravelPlansEndpoint.TravelPointsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Application.TravelPointUpdateRequests.Commands.RejectTravelPointUpdateRequest command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}