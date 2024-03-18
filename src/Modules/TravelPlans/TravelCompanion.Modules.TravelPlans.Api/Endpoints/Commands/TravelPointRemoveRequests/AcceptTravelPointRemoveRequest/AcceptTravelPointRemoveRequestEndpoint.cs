using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.TravelPointRemoveRequests.AcceptTravelPointRemoveRequest;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class AcceptTravelPointRemoveRequestEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPointRemoveRequests.Commands.AcceptTravelPointRemoveRequest>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;
    public AcceptTravelPointRemoveRequestEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPut("Point/Remove/Acceptance/{requestId:guid}")]
    [SwaggerOperation(
        Summary = "Accept Travel Point Remove Request",
        Tags = new[] { TravelPlansEndpoint.TravelPointsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Application.TravelPointRemoveRequests.Commands.AcceptTravelPointRemoveRequest command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}