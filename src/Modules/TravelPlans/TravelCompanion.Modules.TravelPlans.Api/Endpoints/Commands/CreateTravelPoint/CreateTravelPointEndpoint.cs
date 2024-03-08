using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.CreateTravelPoint;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class CreateTravelPointEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPoints.Commands.CreateTravelPoint>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;
    public CreateTravelPointEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("Point")]
    [SwaggerOperation(
        Summary = "Add Travel Point",
        Tags = new[] { TravelPlansEndpoint.TravelPointsTag })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Application.TravelPoints.Commands.CreateTravelPoint command, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return Created();
    }
}