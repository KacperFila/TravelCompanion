using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.RemovePlanAdditionalCost;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class RemovePlanAdditionalCostEndpoint : EndpointBaseAsync
    .WithRequest<Application.Plans.Commands.RemovePlanAdditionalCost>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RemovePlanAdditionalCostEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpDelete("Plan/Receipt/{receiptId:guid}")]
    [SwaggerOperation(
        Summary = "Remove Plan Receipt",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync([FromRoute]Application.Plans.Commands.RemovePlanAdditionalCost command, CancellationToken cancellationToken = new CancellationToken())
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}