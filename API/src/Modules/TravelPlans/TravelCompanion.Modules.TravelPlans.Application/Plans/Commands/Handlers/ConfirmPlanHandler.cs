using TravelCompanion.Modules.TravelPlans.Application.Plans.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public sealed class ConfirmPlanHandler : ICommandHandler<ConfirmPlan>
{
    private readonly IPlansDomainService _plansDomainService;
    private readonly IPlanRepository _planRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IContext _context;
    private readonly Guid _userId;

    public ConfirmPlanHandler(IPlansDomainService plansDomainService, IMessageBroker messageBroker, IContext context, IPlanRepository planRepository)
    {
        _plansDomainService = plansDomainService;
        _messageBroker = messageBroker;
        _context = context;
        _userId = _context.Identity.Id;
        _planRepository = planRepository;
    }

    public async Task HandleAsync(ConfirmPlan command)
    {
        await _plansDomainService.CreateTravelFromPlan(command.planId);

        var plan = await _planRepository.GetLastCreatedForUser(_userId);

        if (plan != null)
        {
            await _messageBroker.PublishAsync(new ActivePlanChanged(_userId, plan.Id));
        }
    }
}