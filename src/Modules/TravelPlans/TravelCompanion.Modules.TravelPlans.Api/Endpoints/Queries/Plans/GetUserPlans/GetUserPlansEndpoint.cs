using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Exceptions;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Queries.Plans.GetUserPlans;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class GetUserPlansEndpoint : EndpointBaseAsync
    .WithRequest<Application.Plans.Queries.GetUserPlans>
    .WithActionResult<Paged<PlanDetailsDTO>>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetUserPlansEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [Authorize]
    [HttpGet("Plan")]
    [SwaggerOperation(
        Summary = "Get User Plans",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag})]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult<Paged<PlanDetailsDTO>>> HandleAsync([FromQuery]Application.Plans.Queries.GetUserPlans query, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.QueryAsync<Paged<PlanDetailsDTO>>(query);
        return Ok(result);
    }
}