﻿using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

public interface IPlansDomainService
{
    Task AddReceiptAsync(Receipt receipt);
    Task<Guid> CheckPlanOwnerAsync(Guid planId);
    Task<List<Guid>> CheckPlanParticipantsAsync(Guid planId);
    Task CreateTravelFromPlan(Guid planId);
}