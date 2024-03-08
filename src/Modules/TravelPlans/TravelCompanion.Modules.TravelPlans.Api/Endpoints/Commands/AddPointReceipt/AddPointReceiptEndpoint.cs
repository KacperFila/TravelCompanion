using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.AddPointReceipt;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class AddPointReceiptEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPoints.Commands.AddPointReceipt>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AddPointReceiptEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("Point/Receipt")]
    [SwaggerOperation(
        Summary = "Add Travel Point Receipt",
        Tags = new[] { TravelPlansEndpoint.TravelPointsTag })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Application.TravelPoints.Commands.AddPointReceipt command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return Created();
    }
}