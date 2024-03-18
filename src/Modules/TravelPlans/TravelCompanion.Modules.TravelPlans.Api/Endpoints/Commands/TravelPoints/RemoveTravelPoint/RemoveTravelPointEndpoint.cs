using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.TravelPoints.RemoveTravelPoint;

[Route(TravelPlansEndpoint.BasePath)]

internal sealed class RemoveTravelPointEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPoints.Commands.RemoveTravelPoint>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RemoveTravelPointEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpDelete("Point/{travelPointId:guid}")]
    [SwaggerOperation(
        Summary = "Remove Travel Point",
        Tags = new[] { TravelPlansEndpoint.TravelPointsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Application.TravelPoints.Commands.RemoveTravelPoint command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}