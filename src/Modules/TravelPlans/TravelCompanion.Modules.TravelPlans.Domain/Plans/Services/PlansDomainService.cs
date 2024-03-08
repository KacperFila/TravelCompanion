using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

public sealed class PlansDomainService : IPlansDomainService
{
    private readonly IReceiptRepository _receiptRepository;
    private readonly IPlanRepository _planRepository;

    public PlansDomainService(IReceiptRepository receiptRepository, IPlanRepository planRepository)
    {
        _receiptRepository = receiptRepository;
        _planRepository = planRepository;
    }

    public async Task AddReceiptAsync(Receipt receipt)
    {
        await _receiptRepository.AddAsync(receipt);
    }

    public async Task<Guid> CheckPlanOwnerAsync(Guid planId)
    {
        var plan = await _planRepository.GetAsync(planId);
        
        if (plan == null)
        {
            throw new PlanNotFoundException(planId);
        }

        return await Task.FromResult(plan.OwnerId);
    }
}