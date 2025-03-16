using TravelCompanion.Modules.TravelPlans.Application.Invitations.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Invitations;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Messaging;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class AcceptInvitationToTravelPlanHandler : ICommandHandler<AcceptInvitation>
{
    private readonly IPlanRepository _planRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly INotificationRealTimeService _notificationService;
    private readonly IContext _context;
    private readonly Guid _userId;

    public AcceptInvitationToTravelPlanHandler(
        IPlanRepository planRepository,
        IInvitationRepository invitationRepository,
        IMessageBroker messageBroker,
        IUsersModuleApi usersModuleApi,
        INotificationRealTimeService notificationService,
        IContext context)
    {
        _planRepository = planRepository;
        _invitationRepository = invitationRepository;
        _messageBroker = messageBroker;
        _usersModuleApi = usersModuleApi;
        _notificationService = notificationService;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(AcceptInvitation command)
    {
        var invitation = await _invitationRepository.GetAsync(command.invitationId);

        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.invitationId);
        }

        if (_userId != invitation.InviteeId)
        {
            throw new UserNotAllowedToManageInvitationException(_userId);
        }
        
        var plan = await _planRepository.GetAsync(invitation.TravelPlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(invitation.TravelPlanId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        var participantRecord = PlanParticipantRecord.Create(invitation.InviteeId, plan.Id);
        plan.AddParticipant(participantRecord);
        await _planRepository.UpdateAsync(plan);

        await _messageBroker.PublishAsync(new ParticipantAddedToPlan(invitation.InviteeId, plan.Id));

        await _invitationRepository.RemoveAsync(command.invitationId);

        var invitee = await _usersModuleApi.GetUserInfo(invitation.InviteeId);

        await _notificationService.SendToAsync(plan.OwnerId.ToString(),
            NotificationMessage.Create(
                "Invitation accepted",
                $"{invitee.UserName} accepted your invitation!"));
    }
}