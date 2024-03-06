using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands;

public record AddPointReceipt(Guid pointId, [FromRoute]decimal amount, [FromBody]List<EntityId> receiptParticipants) : ICommand;
