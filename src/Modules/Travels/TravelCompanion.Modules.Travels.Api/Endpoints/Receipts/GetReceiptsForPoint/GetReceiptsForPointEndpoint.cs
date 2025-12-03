using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Receipts.GetReceiptsForPoint;

[Route(TravelsEndpoint.BasePath)]
internal sealed class GetReceiptsForPointEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly IReceiptRepository _receiptRepository;

    public GetReceiptsForPointEndpoint(IReceiptRepository receiptRepository)
    {
        _receiptRepository = receiptRepository;
    }

    [Authorize]
    [HttpGet("Point/{travelPointId:guid}/Receipts")]
    [SwaggerOperation(
        Summary = "Get Receipts for Point",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] Guid travelPointId,
        CancellationToken cancellationToken = default)
    {
        var receipts = await _receiptRepository.BrowseForPointAsync(travelPointId);
        return Ok(receipts);
    }
}