using System.Numerics;
using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

public sealed class GetUserActivePlanHandler : IQueryHandler<GetUserActivePlan, PlanDetailsDTO>
{
    private readonly IPlanRepository _planRepository;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly IContext _context;
    private readonly Guid _userId;

    public GetUserActivePlanHandler(IPlanRepository planRepository, IContext context, IUsersModuleApi usersModuleApi)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _usersModuleApi = usersModuleApi;
    }

    public async Task<PlanDetailsDTO> HandleAsync(GetUserActivePlan query)
    {
        Console.WriteLine($"QUERY: {query.userId}");
        var userActivePlanId = await _usersModuleApi.GetUserActivePlan(_userId);
        Console.WriteLine($"USERACTIVEPLANID: {userActivePlanId}");

        Console.WriteLine(userActivePlanId);
        if (userActivePlanId is null)
        {
            return new PlanDetailsDTO();
        }

        var plan = await _planRepository.GetAsync((Guid)userActivePlanId);
        var dto = AsPlanDetailsDto(plan);

        Console.WriteLine($"DTO RETURNED: {dto}");

        return dto;
    }

    private static PlanDetailsDTO AsPlanDetailsDto(Plan plan)
    {
        return new PlanDetailsDTO
        {
            AdditionalCostsValue = plan.AdditionalCostsValue.Amount,
            Description = plan.Description,
            From = plan.From,
            To = plan.To,
            Id = plan.Id,
            OwnerId = plan.OwnerId,
            Participants = plan.Participants.Select(x => x.Value).ToList(),
            PlanStatus = plan.PlanStatus,
            Title = plan.Title
        };
    }
}