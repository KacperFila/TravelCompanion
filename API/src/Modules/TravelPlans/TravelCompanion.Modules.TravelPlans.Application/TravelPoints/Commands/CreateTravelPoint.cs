using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands;

public record CreateTravelPoint(Guid travelPlanId, string placeName) : ICommand;
