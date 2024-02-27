using System.Security.Cryptography.X509Certificates;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlanInvitations.Commands.Handlers;

internal sealed class InviteToTravelPlanHandler : ICommandHandler<InviteToTravelPlan>
{
    private readonly ITravelPlanInvitationRepository _travelPlanInvitationRepository;
    private readonly ITravelPlanRepository _travelPlanRepository;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly IContext _context;

    public InviteToTravelPlanHandler(ITravelPlanInvitationRepository travelPlanInvitationRepository, ITravelPlanRepository travelPlanRepository, IUsersModuleApi usersModuleApi, IContext context)
    {
        _travelPlanInvitationRepository = travelPlanInvitationRepository;
        _travelPlanRepository = travelPlanRepository;
        _usersModuleApi = usersModuleApi;
        _context = context;
    }

    public async Task HandleAsync(InviteToTravelPlan command)
    {
        var doesTravelPlanExist = await _travelPlanRepository.ExistAsync(command.travelPlanId);
        var doesUserExist = await _usersModuleApi.CheckIfUserExists(command.userId);

        if (!doesTravelPlanExist)
        {
            throw new TravelPlanNotFoundException(command.travelPlanId);
        }

        if (!doesUserExist)
        {
            throw new UserNotFoundException(command.userId);
        }

        var doesInvitationAlreadyExist = await _travelPlanInvitationRepository
            .ExistsForUserAndTravelPlanAsync(command.userId, command.travelPlanId);

        if (doesInvitationAlreadyExist)
        {
            throw new InvitationAlreadyExistsException(command.userId, command.travelPlanId);
        }

        var travelPlan = await _travelPlanRepository.GetAsync(command.travelPlanId);

        if (command.userId == _context.Identity.Id || travelPlan.ParticipantIds.Contains(command.userId))
        {
            throw new UserAlreadyParticipatesInTravelPlanException(command.userId);
        }

        var invitation = TravelPlanInvitation.Create(command.travelPlanId, command.userId);

        await _travelPlanInvitationRepository.AddInvitationAsync(invitation);
    }
}