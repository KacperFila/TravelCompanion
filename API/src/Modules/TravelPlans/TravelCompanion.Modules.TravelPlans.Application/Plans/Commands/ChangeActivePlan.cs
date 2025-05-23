using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands;
public record ChangeActivePlan(Guid PlanId) : ICommand;