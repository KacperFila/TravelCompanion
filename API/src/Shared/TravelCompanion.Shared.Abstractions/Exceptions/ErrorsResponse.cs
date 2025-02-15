namespace TravelCompanion.Shared.Abstractions.Exceptions;

public record ErrorsResponse(params Error[] Errors);