using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

public sealed class PlansDomainService : IPlansDomainService
{
    private readonly IReceiptRepository _receiptRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;
    private readonly IMessageBroker _messageBroker;

    public PlansDomainService(IReceiptRepository receiptRepository, IPlanRepository planRepository, IContext context, IMessageBroker messageBroker)
    {
        _receiptRepository = receiptRepository;
        _planRepository = planRepository;
        _context = context;
        _messageBroker = messageBroker;
        _userId = _context.Identity.Id;
    }

    public async Task AddReceiptAsync(Receipt receipt)
    {
        await _receiptRepository.AddAsync(receipt);
    }

    public async Task<Guid> CheckPlanOwnerAsync(Guid planId)
    {
        var plan = await _planRepository.GetAsync(planId);

        if (plan is null)
        {
            throw new PlanNotFoundException(planId);
        }

        return await Task.FromResult(plan.OwnerId);
    }

    public async Task<List<Guid>> CheckPlanParticipantsAsync(Guid planId)
    {
        var plan = await _planRepository.GetAsync(planId);

        if (plan is null)
        {
            throw new PlanNotFoundException(planId);
        }

        var participants = plan.Participants.Select(x => x.ParticipantId).ToList();

        return await Task.FromResult(participants);
    }

    public async Task CreateTravelFromPlan(Guid planId)
    {
        var plan = await _planRepository.GetAsync(planId);

        if (plan is null)
        {
            throw new PlanNotFoundException(planId);
        }

        if (plan.OwnerId != _userId)
        {
            throw new UserNotAllowedToChangePlanException(planId);
        }

        //if (!plan.DoesAllParticipantsAccepted)
        //{
        //    throw new PlanNotAcceptedException(planId);
        //}

        var planPointIds = plan.TravelPlanPoints.Select(x => x.Id.Value).ToList();
        var planReceiptIds = plan.AdditionalCosts.Select(x => x.Id.Value).ToList();

        var planAcceptedMessage = new PlanAccepted(
            plan.Id,
            plan.Participants.Select(x => x.ParticipantId),
            plan.OwnerId,
            plan.Title,
            plan.Description ?? string.Empty,
            plan.From,
            plan.To,
            planReceiptIds,
            plan.AdditionalCostsValue.Amount,
            planPointIds,
            plan.TotalCostValue.Amount);
        
        await _messageBroker.PublishAsync(planAcceptedMessage);
    }
}