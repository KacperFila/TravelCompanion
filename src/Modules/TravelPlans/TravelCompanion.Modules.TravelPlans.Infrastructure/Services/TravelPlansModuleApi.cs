using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Shared;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.Services;

public class TravelPlansModuleApi : ITravelPlansModuleApi
{
    private readonly IPlanRepository _planRepository;
    private readonly ITravelPointRepository _pointRepository;
    public TravelPlansModuleApi(IPlanRepository planRepository, ITravelPointRepository pointRepository)
    {
        _planRepository = planRepository;
        _pointRepository = pointRepository;
    }

    public async Task<List<TravelPoint>> GetPlanTravelPointsAsync(Guid planId)
    {
        return await _pointRepository.GetAllForPlanAsync(planId);
    }

    public async Task<List<Receipt>> GetPlanReceiptsAsync(Guid planId)
    {
        var plan = await _planRepository.GetAsync(planId);
        return await Task.FromResult(plan.AdditionalCosts.ToList());
    }
}