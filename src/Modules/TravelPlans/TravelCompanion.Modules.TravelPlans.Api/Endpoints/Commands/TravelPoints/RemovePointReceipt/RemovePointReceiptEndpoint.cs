using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.TravelPoints.RemovePointReceipt;

[Route(TravelPlansEndpoint.BasePath)]

internal sealed class RemovePointReceiptEndpoint : EndpointBaseAsync
    .WithRequest<Application.TravelPoints.Commands.RemovePointReceipt>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RemovePointReceiptEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpDelete("Point/Receipt/{receiptId:guid}")]
    [SwaggerOperation(
        Summary = "Remove Travel Point Receipt",
        Tags = new[] { TravelPlansEndpoint.TravelPointsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute] Application.TravelPoints.Commands.RemovePointReceipt command, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}