using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands;

public record AcceptTravelPlan(Guid travelPlanId) : ICommand;