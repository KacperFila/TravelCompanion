using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.Payments.Application.TravelUserSummaries.Commands;

public record GenerateTravelUserSummary(Guid UserId, Guid TravelId) : ICommand;