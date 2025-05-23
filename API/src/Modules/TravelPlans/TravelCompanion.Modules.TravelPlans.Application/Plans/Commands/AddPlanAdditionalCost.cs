using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands;

public record AddPlanAdditionalCost(Guid PlanId, decimal Amount, string Description) : ICommand;