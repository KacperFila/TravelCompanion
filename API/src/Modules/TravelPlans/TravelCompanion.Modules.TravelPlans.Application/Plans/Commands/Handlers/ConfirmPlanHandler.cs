using TravelCompanion.Modules.TravelPlans.Application.Plans.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.External;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Messaging;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public sealed class ConfirmPlanHandler : ICommandHandler<ConfirmPlan>
{
    private readonly IPlansDomainService _plansDomainService;
    private readonly IPlanRepository _planRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly INotificationRealTimeService  _notificationRealTimeService;
    private readonly IContext _context;
    private readonly Guid _userId;

    public ConfirmPlanHandler(IPlansDomainService plansDomainService, IMessageBroker messageBroker, IPlanRepository planRepository, INotificationRealTimeService notificationRealTimeService, IContext context)
    {
        _plansDomainService = plansDomainService;
        _messageBroker = messageBroker;
        _planRepository = planRepository;
        _notificationRealTimeService = notificationRealTimeService;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(ConfirmPlan command)
    {
        var plan = await _planRepository.GetAsync(command.PlanId);
        var participantIds = plan.Participants.Select(p => p.ParticipantId).ToList();

        if (plan.OwnerId != _userId)
        {
            await _notificationRealTimeService.SendToAsync(
                _userId,
                NotificationMessage.Create(
                    "Plan can't be accepted",
                    $"You aren't owner of plan: {plan.Title}.",
                    NotificationSeverity.Error));
            
            throw new UserNotOwnerOfPlanException(_userId);
        }
        
        await _plansDomainService.CreateTravelFromPlan(command.PlanId);

        var eligiblePlans = await _planRepository.GetPlansForParticipantsExcludingPlan(participantIds, command.PlanId);

        if (participantIds.Any() && eligiblePlans.Any())
        {
            await AssignNewActivePlansForUsers(participantIds, eligiblePlans);
        }
        
        await _notificationRealTimeService.SendToGroup(participantIds,
            NotificationMessage.Create(
                "Plan accepted",
                $"Plan: {plan.Title} has been accepted",
                NotificationSeverity.Information)
        );
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