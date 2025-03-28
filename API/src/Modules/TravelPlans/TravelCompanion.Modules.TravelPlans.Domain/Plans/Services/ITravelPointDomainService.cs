﻿using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

public interface ITravelPointDomainService
{
    Task AddReceiptAsync(TravelPointId pointId, decimal amount, List<Guid> receiptParticipants, string description);
    Task RemoveReceiptAsync(Guid receiptId);
    Task RemoveTravelPointRemoveRequest(Guid requestId);
}