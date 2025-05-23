using TravelCompanion.Modules.TravelPlans.Application.Invitations.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Queries.Handlers;

internal sealed class GetUserInvitationsHandler : IQueryHandler<GetUserInvitations, List<InvitationDto>>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IUsersModuleApi _userModuleApi;
    private readonly IContext _context;
    private readonly Guid _userId;

    public GetUserInvitationsHandler(IInvitationRepository invitationRepository, IPlanRepository planRepository, IContext context, IUsersModuleApi userModuleApi)
    {
        _invitationRepository = invitationRepository;
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _userModuleApi = userModuleApi;
    }

    public async Task<List<InvitationDto>> HandleAsync(GetUserInvitations query)
    {
        var invitations = await _invitationRepository.GetInvitationsForUser(_userId);
        var invitationsDto = invitations.Select(AsInvitationDto).ToList();

        var planIds = invitationsDto
            .Select(x => x.PlanId)
            .Distinct()
            .ToList();

        var plans = await _planRepository.BrowseById(planIds);

        foreach (var invitation in invitationsDto)
        {
            var plan = plans.FirstOrDefault(x => x.Id == invitation.PlanId);
            if (plan is not null)
            {
                var owner = await _userModuleApi.GetUserInfo(plan.OwnerId);
                invitation.PlanTitle = plan.Title;
                invitation.InviterName = owner.UserName;
            }
        }

        return invitationsDto;
    }

    private static InvitationDto AsInvitationDto(Invitation invitation)
    {
        return new InvitationDto()
        {
            InvitationId = invitation.Id,
            PlanId = invitation.TravelPlanId,
            InvitationDate = invitation.CreatedOnUtc
        };
    }
}
