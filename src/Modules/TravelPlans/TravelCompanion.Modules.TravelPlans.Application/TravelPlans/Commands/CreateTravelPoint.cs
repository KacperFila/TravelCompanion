using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlans.Commands;

public record CreateTravelPoint(Guid travelPlanId, string PlaceName) : ICommand;
