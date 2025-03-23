using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.External;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

public class TravelPointDomainService : ITravelPointDomainService
{
    private readonly IReceiptRepository _receiptRepository;
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly ITravelPointRemoveRequestRepository _travelPointRemoveRequestRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;
    private readonly IMessageBroker _messageBroker;
    public TravelPointDomainService(
        IReceiptRepository receiptRepository,
        ITravelPointRepository travelPointRepository,
        IPlanRepository planRepository,
        IContext context,
        IMessageBroker messageBroker,
        ITravelPointRemoveRequestRepository travelPointRemoveRequestRepository)
    {
        _receiptRepository = receiptRepository;
        _travelPointRepository = travelPointRepository;
        _planRepository = planRepository;
        _context = context;
        _messageBroker = messageBroker;
        _travelPointRemoveRequestRepository = travelPointRemoveRequestRepository;
        _userId = _context.Identity.Id;
    }

    public async Task AddReceiptAsync(TravelPointId pointId, decimal amount, List<Guid> receiptParticipants, string description)
    {
        var point = await _travelPointRepository.GetAsync(pointId);

        if (point == null)
        {
            throw new TravelPointNotFoundException(pointId);
        }

        if (!point.IsAccepted)
        {
            throw new CouldNotModifyAcceptedTravelPointException();
            ;
        }

        var plan = await _planRepository.GetAsync(point.PlanId);

        if (plan == null)
        {
            throw new PlanNotFoundException(point.PlanId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        foreach (var receiptParticipant in receiptParticipants)
        {
            if (!plan.Participants.Any(x => x.ParticipantId == _userId))
            {
                throw new UserDoesNotParticipateInPlanException(receiptParticipant, plan.Id);
            }
        }

        var receipt = Receipt.Create(
            _userId,
            receiptParticipants,
            Money.Create(amount),
            null,
            new AggregateId(pointId),
            description);

        point.AddReceipt(receipt);
        await _travelPointRepository.UpdateAsync(point);
        await _messageBroker.PublishAsync(new PointReceiptAdded(plan.Id, amount));
    }

    public async Task RemoveReceiptAsync(Guid receiptId)
    {
        var receipt = await _receiptRepository.GetAsync(receiptId);

        if (receipt == null)
        {
            throw new ReceiptNotFoundException(receipt.Id);
        }

        var point = await _travelPointRepository.GetAsync(receipt.PointId);
        var plan = await _planRepository.GetAsync(point.PlanId);

        if (plan.OwnerId != _userId)
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        point.RemoveReceipt(receipt);

        await _travelPointRepository.UpdateAsync(point);
        await _receiptRepository.RemoveAsync(receipt);
    }

    public async Task RemoveTravelPointRemoveRequest(Guid requestId)
    {
        var request = await _travelPointRemoveRequestRepository.GetAsync(requestId);

        if (request is null)
        {
            throw new TravelPointRemoveRequestNotFoundException(requestId);
        }

        var point = await _travelPointRepository.GetAsync(request.TravelPointId);

        if (point is null)
        {
            throw new TravelPointNotFoundException(request.TravelPointId);
        }

        var plan = await _planRepository.GetAsync(point.PlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(point.PlanId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        if (plan.OwnerId != _userId)
        {
            throw new UserNotOwnerOfPlanException(_userId);
        }

        await _travelPointRemoveRequestRepository.RemoveAsync(request);
    }
}