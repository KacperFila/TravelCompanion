using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands;

public record AddPointReceipt(Guid pointId, decimal amount, List<Guid> receiptParticipants, string description) : ICommand;
