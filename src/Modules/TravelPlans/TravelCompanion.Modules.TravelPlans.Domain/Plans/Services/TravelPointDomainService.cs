using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

public class TravelPointDomainService : ITravelPointDomainService
{
    private readonly IReceiptRepository _receiptRepository;
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;

    public TravelPointDomainService(IReceiptRepository receiptRepository, ITravelPointRepository travelPointRepository, IPlanRepository planRepository)
    {
        _receiptRepository = receiptRepository;
        _travelPointRepository = travelPointRepository;
        _planRepository = planRepository;
    }

    public async Task AddReceiptAsync(TravelPointId pointId, decimal amount, List<EntityId> receiptParticipants)
    {
        var point = await _travelPointRepository.GetAsync(pointId);
        
        if (point == null)
        {
            throw new TravelPointNotFoundException(pointId);
        }

        var plan = await _planRepository.GetAsync(point.PlanId);

        if (plan == null)
        {
            throw new PlanNotFoundException(point.PlanId);
        }

        foreach (var receiptParticipant in receiptParticipants)
        {
            if (!plan.Participants.Contains(receiptParticipant))
            {
                throw new UserDoesNotParticipateInPlanException(receiptParticipant, plan.Id);
            }
        }

        point.AddReceipt(pointId, amount, receiptParticipants);
        point.CalculateCost();

        await _travelPointRepository.UpdateAsync(point);
    }
}