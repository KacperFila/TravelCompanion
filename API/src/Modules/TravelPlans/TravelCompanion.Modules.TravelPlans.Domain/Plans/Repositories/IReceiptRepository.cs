using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface IReceiptRepository
{
    Task AddAsync(Receipt receipt);
    Task<Receipt> GetAsync(Guid receiptId);
    Task<List<Receipt>> BrowseForPlanAsync(Guid planId);
    Task<List<Receipt>> BrowseForPointAsync(Guid pointId);
    Task RemoveAsync(Receipt receipt);
}