using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.External;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Invitations;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class InviteToTravelPlanHandler : ICommandHandler<InviteToTravelPlan>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly INotificationRealTimeService _notificationService;

    public InviteToTravelPlanHandler(
        IInvitationRepository invitationRepository,
        IPlanRepository planRepository,
        IUsersModuleApi usersModuleApi,
        INotificationRealTimeService notificationService)
    {
        _invitationRepository = invitationRepository;
        _planRepository = planRepository;
        _usersModuleApi = usersModuleApi;
        _notificationService = notificationService;
    }

    public async Task HandleAsync(InviteToTravelPlan command)
    {
        var doesPlanExist = await _planRepository.ExistAsync(command.planId);
        var doesUserExist = await _usersModuleApi.CheckIfUserExists(command.userId);

        if (!doesPlanExist)
        {
            throw new PlanNotFoundException(command.planId);
        }

        if (!doesUserExist)
        {
            throw new UserNotFoundException(command.userId);
        }

        var doesInvitationAlreadyExist = await _invitationRepository
            .ExistsForUserAndTravelPlanAsync(command.userId, command.planId);

        if (doesInvitationAlreadyExist)
        {
            throw new InvitationAlreadyExistsException(command.userId, command.planId);
        }

        var plan = await _planRepository.GetAsync(command.planId);

        if (plan.Participants.Select(x => x.ParticipantId).Contains(command.userId))
        {
            throw new UserAlreadyParticipatesInPlanException(command.userId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        var invitation = Invitation.Create(command.planId, command.userId);

        await _invitationRepository.AddAsync(invitation);

        await _notificationService.SendToAsync(command.userId.ToString(),
            NotificationMessage.Create(
                "Invitation",
                $"You have been invited to plan: {plan.Title}",
                plan.OwnerId.ToString()));
    }
}