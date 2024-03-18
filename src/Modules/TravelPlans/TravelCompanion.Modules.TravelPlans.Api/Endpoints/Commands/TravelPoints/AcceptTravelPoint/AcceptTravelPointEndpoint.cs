using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.TravelPoints.AcceptTravelPoint;

[Route(TravelPlansEndpoint.BasePath)]

internal sealed class AcceptTravelPointEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPoints.Commands.AcceptTravelPoint>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AcceptTravelPointEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPut("Point/Acceptance/{pointId:guid}")]
    [SwaggerOperation(
        Summary = "Accept Travel Point",
        Tags = new[] { TravelPlansEndpoint.TravelPointsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Application.TravelPoints.Commands.AcceptTravelPoint command, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}