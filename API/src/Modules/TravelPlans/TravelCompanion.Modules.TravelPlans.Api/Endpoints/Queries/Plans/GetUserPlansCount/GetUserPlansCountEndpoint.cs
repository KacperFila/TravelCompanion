using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Shared.Abstractions.Exceptions;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Queries.Plans.GetUserPlansCount;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class GetUserPlansCountEndpoint : EndpointBaseAsync
    .WithRequest<Application.Plans.Queries.GetUserPlansCount>
    .WithResult<int>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetUserPlansCountEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [Authorize]
    [HttpGet("Plan/Count")]
    [SwaggerOperation(
        Summary = "Get User Plans Count",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<int> HandleAsync([FromQuery] Application.Plans.Queries.GetUserPlansCount query, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _queryDispatcher.QueryAsync(query);
        return await Task.FromResult(result);
    }
}