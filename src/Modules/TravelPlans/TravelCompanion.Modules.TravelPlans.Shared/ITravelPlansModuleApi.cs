using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Shared;

public interface ITravelPlansModuleApi
{
    Task<List<TravelPoint>> GetPlanTravelPointsAsync(Guid planId);
    Task<List<Receipt>> GetPlanReceiptsAsync(Guid planId);
}