using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Commands.Plans.CreateTravelPlan;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class CreateTravelPlanEndpoint : EndpointBaseAsync
    .WithRequest<Application.Plans.Commands.CreateTravelPlan>
    .WithActionResult
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CreateTravelPlanEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPost("Plan")]
    [SwaggerOperation(
        Summary = "Create Travel Plan",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(Application.Plans.Commands.CreateTravelPlan command, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.SendAsync(command);
        return Created();
    }
}