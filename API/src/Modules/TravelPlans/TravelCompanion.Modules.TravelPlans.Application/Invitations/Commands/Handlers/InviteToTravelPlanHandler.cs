using TravelCompanion.Modules.TravelPlans.Application.Invitations.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.External;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class InviteToTravelPlanHandler : ICommandHandler<InviteToTravelPlan>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly INotificationRealTimeService _notificationService;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;
    private readonly IContext _context;
    private readonly Guid _userId;

    public InviteToTravelPlanHandler(
        IInvitationRepository invitationRepository,
        IPlanRepository planRepository,
        IUsersModuleApi usersModuleApi,
        INotificationRealTimeService notificationService,
        ITravelPlansRealTimeService travelPlansRealTimeService,
        IContext context)
    {
        _invitationRepository = invitationRepository;
        _planRepository = planRepository;
        _usersModuleApi = usersModuleApi;
        _notificationService = notificationService;
        _travelPlansRealTimeService = travelPlansRealTimeService;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(InviteToTravelPlan command)
    {
        var doesPlanExist = await _planRepository.ExistAsync(command.PlanId);
        var doesUserExist = await _usersModuleApi.CheckIfUserExists(command.InviteeId);

        if (!doesPlanExist)
        {
            throw new PlanNotFoundException(command.PlanId);
        }

        if (!doesUserExist)
        {
            throw new UserNotFoundException(command.InviteeId);
        }

        var doesInvitationAlreadyExist = await _invitationRepository
            .ExistsForUserAndTravelPlanAsync(command.InviteeId, command.PlanId);

        if (doesInvitationAlreadyExist)
        {
            await _notificationService.SendToAsync(
            _userId.ToString(),
            NotificationMessage.Create(
                "Invitation",
                $"You have already invited given user!",
            NotificationSeverity.Error));

            return;
        }

        var plan = await _planRepository.GetAsync(command.PlanId);

        if (plan.Participants.Select(x => x.ParticipantId).Contains(command.InviteeId))
        {
            await _notificationService.SendToAsync(
            _userId.ToString(),
            NotificationMessage.Create(
                "Invitation",
                $"User already participates in this plan!",
            NotificationSeverity.Error));

            throw new UserAlreadyParticipatesInPlanException(command.InviteeId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        var invitation = Invitation.Create(command.PlanId, command.InviteeId);

        await _invitationRepository.AddAsync(invitation);

        await _notificationService.SendToAsync(
            command.InviteeId.ToString(),
            NotificationMessage.Create(
                "Invitation",
                $"You have been invited to plan: \"{plan.Title}\"",
                _context.Identity.Email,
            NotificationSeverity.Alert));

        await _notificationService.SendToAsync(
            _userId.ToString(),
            NotificationMessage.Create(
                "Invitation",
                $"Invitation has been sent!",
                _context.Identity.Email,
            NotificationSeverity.Information));

        var planOwnerInfo =  await _usersModuleApi.GetUserInfo(plan.OwnerId);

        var invitationResponse = new InvitationDTO()
        {
            InvitationId = invitation.Id,
            PlanId = plan.Id,
            PlanTitle = plan.Title,
            InviterName = planOwnerInfo.Email,
            InvitationDate = DateTime.UtcNow
        };

        await _travelPlansRealTimeService.SendPlanInvitation(command.InviteeId.ToString(), invitationResponse);
    }
}