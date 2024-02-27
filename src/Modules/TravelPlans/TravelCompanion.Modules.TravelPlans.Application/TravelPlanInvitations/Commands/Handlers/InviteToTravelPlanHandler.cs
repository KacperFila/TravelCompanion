using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlanInvitations.Commands.Handlers;

internal sealed class InviteToTravelPlanHandler : ICommandHandler<InviteToTravelPlan>
{
    private readonly ITravelPlanInvitationRepository _travelPlanInvitationRepository;
    private readonly ITravelPlanRepository _travelPlanRepository;
    private readonly IUsersModuleApi _usersModuleApi;

    public InviteToTravelPlanHandler(ITravelPlanInvitationRepository travelPlanInvitationRepository, ITravelPlanRepository travelPlanRepository, IUsersModuleApi usersModuleApi)
    {
        _travelPlanInvitationRepository = travelPlanInvitationRepository;
        _travelPlanRepository = travelPlanRepository;
        _usersModuleApi = usersModuleApi;
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

        var invitation = TravelPlanInvitation.Create(command.travelPlanId, command.userId);

        await _travelPlanInvitationRepository.AddInvitationAsync(invitation);
    }
}