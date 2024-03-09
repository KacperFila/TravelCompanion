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

    public async Task AcceptTravelPlan(Guid planId)
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

        

        await _planRepository.DeleteAsync(plan.Id);

        await _messageBroker.PublishAsync(
            new PlanAccepted(
                plan.Participants.Select(x => x.Value).ToList(),
                plan.OwnerId,
                plan.Title,
                plan.Description,
                plan.From,
                plan.To,
                plan.AdditionalCosts,
                plan.AdditionalCostsValue.Amount,
                plan.TravelPlanPoints));
    }
}