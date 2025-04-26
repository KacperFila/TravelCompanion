using TravelCompanion.Modules.TravelPlans.Application.Invitations.DTO;
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
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class AcceptInvitationToTravelPlanHandler : ICommandHandler<AcceptInvitation>
{
    private readonly IPlanRepository _planRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly INotificationRealTimeService _notificationService;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;
    private readonly IContext _context;
    private readonly Guid _userId;

    public AcceptInvitationToTravelPlanHandler(
        IPlanRepository planRepository,
        IInvitationRepository invitationRepository,
        IMessageBroker messageBroker,
        IUsersModuleApi usersModuleApi,
        INotificationRealTimeService notificationService,
        IContext context,
        ITravelPlansRealTimeService travelPlansRealTimeService)
    {
        _planRepository = planRepository;
        _invitationRepository = invitationRepository;
        _messageBroker = messageBroker;
        _usersModuleApi = usersModuleApi;
        _notificationService = notificationService;
        _context = context;
        _userId = _context.Identity.Id;
        _travelPlansRealTimeService = travelPlansRealTimeService;
    }

    public async Task HandleAsync(AcceptInvitation command)
    {
        var invitation = await _invitationRepository.GetAsync(command.invitationId);

        var inviteeId = invitation.InviteeId;

        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.invitationId);
        }

        if (_userId != inviteeId)
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

        var participantRecord = PlanParticipantRecord.Create(inviteeId, plan.Id);
        plan.AddParticipant(participantRecord);
        await _planRepository.UpdateAsync(plan);

        await _messageBroker.PublishAsync(new ParticipantAddedToPlan(inviteeId, plan.Id));

        await _invitationRepository.RemoveAsync(command.invitationId);

        var invitee = await _usersModuleApi.GetUserInfo(inviteeId);

        await _notificationService.SendToAsync(plan.OwnerId.ToString(),
            NotificationMessage.Create(
                "Invitation accepted",
                $"\"{invitee.UserName}\" accepted your invitation!",
                _context.Identity.Email,
            NotificationSeverity.Alert));

        var invitationRemovedResponse = new PlanInvitationRemovedResponse()
        {
            InvitationId = command.invitationId,
        };

        await _travelPlansRealTimeService.SendPlanInvitationRemoved(inviteeId.ToString(), invitationRemovedResponse);
    }
}