using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.Payments.Application.TravelSummaries.Commands;

public record GenerateTravelSummary(Guid TravelId) : ICommand;