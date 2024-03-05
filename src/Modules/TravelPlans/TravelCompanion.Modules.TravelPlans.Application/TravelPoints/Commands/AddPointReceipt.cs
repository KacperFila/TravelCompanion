using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands;

public record AddPointReceipt(Guid pointId, decimal amount, Guid participantId) : ICommand;
