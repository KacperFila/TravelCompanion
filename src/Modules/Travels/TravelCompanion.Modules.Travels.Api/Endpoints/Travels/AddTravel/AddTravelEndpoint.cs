using Ardalis.ApiEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Modules.Travels.Core.Validators;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.AddTravel;

[Route(TravelsEndpoint.BasePath)]

internal sealed class AddTravelEndpoint : EndpointBaseAsync
    .WithRequest<TravelUpsertDTO>
    .WithActionResult
{
    private readonly ITravelService _travelService;
    private readonly TravelDtoValidator _travelDtoValidator = new();

    public AddTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Add Travel",
        Tags = new[] { TravelsEndpoint.TravelsTag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public override async Task<ActionResult> HandleAsync(TravelUpsertDTO request, CancellationToken cancellationToken = default)
    {
        await _travelDtoValidator.ValidateAndThrowAsync(request, cancellationToken);
        await _travelService.AddAsync(request);

        return Created();
    }
}