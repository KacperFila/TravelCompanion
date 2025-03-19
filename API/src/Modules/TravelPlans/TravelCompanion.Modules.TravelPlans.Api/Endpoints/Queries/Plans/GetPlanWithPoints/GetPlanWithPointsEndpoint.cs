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
internal sealed class GetTravelPointUpdateRequestsEndpoint : EndpointBaseAsync
    .WithRequest<Application.Plans.Queries.GetPlanWithPoints>
    .WithActionResult<PlanWithPointsDTO>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetTravelPointUpdateRequestsEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [Authorize]
    [HttpGet("Plan/{planId:guid}/Points")]
    [SwaggerOperation(
        Summary = "Get Points For Plan",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult<PlanWithPointsDTO>> HandleAsync([FromRoute] Application.Plans.Queries.GetPlanWithPoints query, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }
}