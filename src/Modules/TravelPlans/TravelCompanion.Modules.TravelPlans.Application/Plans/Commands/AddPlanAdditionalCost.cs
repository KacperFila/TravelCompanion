using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands;

public record AddPlanAdditionalCost(Guid planId, decimal amount, string description) : ICommand;