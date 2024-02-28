using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands;

public record ChangeTravelPoint(Guid travelPointId, string placeName) : ICommand;
