using Ardalis.ApiEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Services;
using TravelCompanion.Modules.Travels.Core.Validators;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.AddTravel;

[Route(TravelsEndpoint.BasePath)]

internal sealed class AddTravelEndpoint : EndpointBaseAsync
    .WithRequest<TravelDto>
    .WithActionResult
{
    private readonly ITravelService _travelService;
    private readonly TravelDtoValidator _travelDtoValidator = new();

    public AddTravelEndpoint(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpPost]
    public override async Task<ActionResult> HandleAsync(TravelDto request, CancellationToken cancellationToken = default)
    {
        await _travelDtoValidator.ValidateAndThrowAsync(request, cancellationToken);
        await _travelService.AddAsync(request);

        return Created();
    }
}