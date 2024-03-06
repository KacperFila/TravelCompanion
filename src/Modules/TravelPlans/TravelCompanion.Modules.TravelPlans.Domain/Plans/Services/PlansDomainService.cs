using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

public sealed class PlansDomainService : IPlansDomainService
{
    private readonly IReceiptRepository _receiptRepository;

    public PlansDomainService(IReceiptRepository receiptRepository)
    {
        _receiptRepository = receiptRepository;
    }

    public async Task AddReceiptAsync(Receipt receipt)
    {
        await _receiptRepository.AddAsync(receipt);
    }
}