using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Shared.Abstractions.Exceptions;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Queries.Plans.GetPlanWithPoints;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class GetUserActivePlanEndpoint : EndpointBaseAsync
    .WithRequest<Application.Plans.Queries.GetUserActivePlan>
    .WithActionResult<PlanWithPointsDTO>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetUserActivePlanEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [Authorize]
    [HttpGet("Plan/Active")]
    [SwaggerOperation(
        Summary = "Get Active Plan for User",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult<PlanWithPointsDTO>> HandleAsync([FromRoute] Application.Plans.Queries.GetUserActivePlan query, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }
}