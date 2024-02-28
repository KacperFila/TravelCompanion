using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class InviteToTravelPlanHandler : ICommandHandler<InviteToTravelPlan>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly IContext _context;

    public InviteToTravelPlanHandler(IInvitationRepository invitationRepository, IPlanRepository planRepository, IUsersModuleApi usersModuleApi, IContext context)
    {
        _invitationRepository = invitationRepository;
        _planRepository = planRepository;
        _usersModuleApi = usersModuleApi;
        _context = context;
    }

    public async Task HandleAsync(InviteToTravelPlan command)
    {
        var doesTravelPlanExist = await _planRepository.ExistAsync(command.travelPlanId);
        var doesUserExist = await _usersModuleApi.CheckIfUserExists(command.userId);

        if (!doesTravelPlanExist)
        {
            throw new PlanNotFoundException(command.travelPlanId);
        }

        if (!doesUserExist)
        {
            throw new UserNotFoundException(command.userId);
        }

        var doesInvitationAlreadyExist = await _invitationRepository
            .ExistsForUserAndTravelPlanAsync(command.userId, command.travelPlanId);

        if (doesInvitationAlreadyExist)
        {
            throw new InvitationAlreadyExistsException(command.userId, command.travelPlanId);
        }

        var travelPlan = await _planRepository.GetAsync(command.travelPlanId);

        if (command.userId == _context.Identity.Id || travelPlan.ParticipantIds.Contains(command.userId))
        {
            throw new UserAlreadyParticipatesInPlanException(command.userId);
        }

        var invitation = Invitation.Create(command.travelPlanId, command.userId);

        await _invitationRepository.AddAsync(invitation);
    }
}