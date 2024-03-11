using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands;

public record AddPlanAdditionalCost(Guid planId, decimal amount, string description) : ICommand;