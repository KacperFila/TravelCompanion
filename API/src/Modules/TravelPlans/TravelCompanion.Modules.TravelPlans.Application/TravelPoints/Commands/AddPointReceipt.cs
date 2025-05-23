using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands;

public record AddPointReceipt(Guid PointId, decimal Amount, List<Guid> ReceiptParticipants, string Description) : ICommand;
