using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Api.Endpoints.Receipts.AddPointReceipt;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Shared.Abstractions.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.AddPointReceipt;

[Route(TravelsEndpoint.BasePath)]
internal sealed class AddPointReceiptEndpoint : EndpointBaseAsync
    .WithRequest<AddPointReceiptRequest>
    .WithActionResult
{
    private readonly ITravelService _travelService;

    public AddPointReceiptEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [Authorize]
    [HttpPost("Point/Receipt/{travelPointId}")]
    [SwaggerOperation(
        Summary = "Add Travel Point Receipt",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(AddPointReceiptRequest request, CancellationToken cancellationToken = default)
    {
        await _travelService.AddReceipt(
            request.TravelPointId,
            request.ParticipantsIds,
            Money.Create(request.Amount),
            request.Description);

        return Created();
    }
}