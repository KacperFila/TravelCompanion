using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.CreateAcceptPlanRequest;

internal sealed class CreateAcceptPlanRequestEndpoint : EndpointBaseAsync
    .WithRequest<Application.AcceptPlanRequests.Commands.CreateAcceptPlanRequest>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CreateAcceptPlanRequestEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPost("Plan/Acceptance/{planId:guid}/Request")]
    [SwaggerOperation(
        Summary = "Create Accept Travel Request",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag})]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute]Application.AcceptPlanRequests.Commands.CreateAcceptPlanRequest command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}