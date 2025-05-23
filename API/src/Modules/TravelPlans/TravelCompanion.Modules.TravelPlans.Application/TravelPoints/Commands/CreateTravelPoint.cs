using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands;

public record CreateTravelPoint(Guid TravelPlanId, string PlaceName) : ICommand;
