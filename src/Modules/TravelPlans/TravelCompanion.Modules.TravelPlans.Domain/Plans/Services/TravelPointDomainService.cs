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
    private readonly IContext _context;
    private readonly Guid _userId;

    public TravelPointDomainService(IReceiptRepository receiptRepository, ITravelPointRepository travelPointRepository, IPlanRepository planRepository, IContext context)
    {
        _receiptRepository = receiptRepository;
        _travelPointRepository = travelPointRepository;
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task AddReceiptAsync(TravelPointId pointId, decimal amount, List<Guid> receiptParticipants, string description)
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

        point.AddReceipt(pointId, amount, receiptParticipants, description);
        point.CalculateCost();

        await _travelPointRepository.UpdateAsync(point);
    }

    public async Task RemoveReceiptAsync(Guid receiptId)
    {
        var receipt = await _receiptRepository.GetAsync(receiptId);

        if (receipt == null)
        {
            throw new ReceiptNotFoundException();
        }

        var point = await _travelPointRepository.GetAsync(receipt.PointId);

        var plan = await _planRepository.GetAsync(point.PlanId);

        if (plan.OwnerId != _userId)
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        point.RemoveReceipt(receipt);
        point.CalculateCost();

        await _travelPointRepository.UpdateAsync(point);
        await _receiptRepository.RemoveAsync(receipt);
    }
}