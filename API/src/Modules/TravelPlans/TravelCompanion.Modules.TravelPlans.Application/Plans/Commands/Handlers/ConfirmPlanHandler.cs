using TravelCompanion.Modules.TravelPlans.Application.Plans.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
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

    public ConfirmPlanHandler(IPlansDomainService plansDomainService, IMessageBroker messageBroker, IContext context, IPlanRepository planRepository)
    {
        _plansDomainService = plansDomainService;
        _messageBroker = messageBroker;
        _context = context;
        _planRepository = planRepository;
    }

    public async Task HandleAsync(ConfirmPlan command)
    {
        var plan = await _planRepository.GetAsync(command.planId);
        var participantIds = plan.Participants.Select(p => p.ParticipantId).ToList();

        await _plansDomainService.CreateTravelFromPlan(command.planId);

        var eligiblePlans = await _planRepository.GetPlansForParticipantsExcludingPlan(participantIds, command.planId);

        if (participantIds.Any() && eligiblePlans.Any())
        {
            await AssignNewActivePlansForUsers(participantIds, eligiblePlans);
        }
    }

    private async Task AssignNewActivePlansForUsers(List<Guid> usersIds, List<Plan> plans)
    {
        var tasks = usersIds.Select(userId =>
        {
            var randomPlan = plans
                .Where(p => p.Participants.Any(pp => pp.ParticipantId == userId))
                .OrderByDescending(x => x.CreatedOnUtc)
                .FirstOrDefault();

            return _messageBroker.PublishAsync(new ActivePlanChanged(userId, randomPlan?.Id));
        });
        
        await Task.WhenAll(tasks);
    }
}