using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.TravelPlans.Application.AcceptPlanRequests.Commands;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.AcceptPlanRequests.UndoAcceptPlanAcceptRequest;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class UndoAcceptPlanAcceptRequestEndpoint : EndpointBaseAsync
    .WithRequest<DenyPlanAcceptRequest>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public UndoAcceptPlanAcceptRequestEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpDelete("Plan/Acceptance/{travelPlanId:guid}")]
    [SwaggerOperation(
        Summary = "Undo Accept Travel Plan",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] DenyPlanAcceptRequest command, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}