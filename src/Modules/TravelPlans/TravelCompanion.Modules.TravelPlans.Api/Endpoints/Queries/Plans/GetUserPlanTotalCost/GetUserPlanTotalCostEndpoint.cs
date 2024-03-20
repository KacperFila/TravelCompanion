using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Queries.Plans.GetUserPlanTotalCost;

internal sealed class GetUserPlanTotalCostEndpoint : EndpointBaseAsync
    .WithRequest<Application.Plans.Queries.GetUserPlanTotalCost>
    .WithResult<Money>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetUserPlanTotalCostEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [Authorize]
    [HttpGet("Plan/{planId:guid}/Cost")]
    [SwaggerOperation(
        Summary = "Get User Plan Cost",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<Money> HandleAsync([FromRoute]Application.Plans.Queries.GetUserPlanTotalCost query, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        return await Task.FromResult(result);
    }
}