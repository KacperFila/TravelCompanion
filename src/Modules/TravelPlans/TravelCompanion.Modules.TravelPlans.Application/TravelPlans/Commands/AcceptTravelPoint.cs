using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlans.Commands;

public record AcceptTravelPoint(Guid travelPointId) : ICommand;
