﻿using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface IReceiptRepository
{
    Task AddAsync(Receipt receipt);
}