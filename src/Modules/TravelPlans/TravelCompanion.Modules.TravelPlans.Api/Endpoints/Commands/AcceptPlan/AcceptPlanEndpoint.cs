using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.AcceptPlan;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class AcceptPlanEndpoint : EndpointBaseAsync
    .WithRequest<Application.AcceptPlanRequests.Commands.AcceptPlanAcceptRequest>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AcceptPlanEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPut("Plan/Acceptance/{travelPlanId:guid}")]
    [SwaggerOperation(
        Summary = "Accept Travel Plan",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag})]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute]Application.AcceptPlanRequests.Commands.AcceptPlanAcceptRequest command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}