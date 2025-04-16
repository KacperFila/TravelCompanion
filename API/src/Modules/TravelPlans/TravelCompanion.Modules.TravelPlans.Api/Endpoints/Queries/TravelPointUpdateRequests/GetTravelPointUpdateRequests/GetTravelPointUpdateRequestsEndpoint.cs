using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Queries;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Exceptions;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Queries.TravelPointUpdateRequests.GetTravelPointUpdateRequests;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class GetTravelPointUpdateRequestsEndpoint : EndpointBaseAsync
    .WithRequest<GetTravelPointUpdateRequest>
    .WithActionResult<List<TravelPointUpdateRequest>>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetTravelPointUpdateRequestsEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [Authorize]
    [HttpGet("Point/{planId:guid}/UpdateRequests")]
    [SwaggerOperation(
        Summary = "Get Update Requests For Plan",
        Tags = new[] { TravelPlansEndpoint.TravelPlansTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult<List<TravelPointUpdateRequest>>> HandleAsync([FromRoute] GetTravelPointUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.QueryAsync(request);
        return Ok(result);
    }
}