using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.TravelPlans.Application.Invitations.DTO;
using TravelCompanion.Modules.TravelPlans.Application.Invitations.Queries;
using TravelCompanion.Shared.Abstractions.Exceptions;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Api.Endpoints.Queries.Plans.GetPlanWithPoints;

[Route(TravelPlansEndpoint.BasePath)]
internal sealed class GetUserInvitationsEndpoint : EndpointBaseAsync
    .WithRequest<GetUserInvitations>
    .WithActionResult<InvitationDTO>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetUserInvitationsEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [Authorize]
    [HttpGet("Invitation")]
    [SwaggerOperation(
        Summary = "Get Invitations For User",
        Tags = new[] { TravelPlansEndpoint.InvitationsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async override Task<ActionResult<InvitationDTO>> HandleAsync([FromRoute] GetUserInvitations query, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }
}