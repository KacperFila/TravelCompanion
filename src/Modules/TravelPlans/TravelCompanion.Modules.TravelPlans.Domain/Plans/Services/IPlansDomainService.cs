using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

public interface IPlansDomainService
{
    Task AddReceiptAsync(Receipt receipt);
}