using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

internal sealed class GetUserActivePlanHandler : IQueryHandler<GetUserActivePlan, PlanWithPointsDTO>
{
    private readonly IPlanRepository _planRepository;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly IContext _context;
    private readonly Guid _userId;

    public GetUserActivePlanHandler(
        IPlanRepository planRepository,
        IContext context,
        IUsersModuleApi usersModuleApi)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _usersModuleApi = usersModuleApi;
    }

    public async Task<PlanWithPointsDTO> HandleAsync(GetUserActivePlan query)
    {
        var userInfo = await _usersModuleApi.GetUserInfo(_userId);

        if (userInfo.ActivePlanId is null)
        {
            throw new NoActivePlanForUserException(_userId);
        }

        var plan = await _planRepository.GetAsync((Guid)userInfo.ActivePlanId);

        if (plan is null)
        {
            throw new NoActivePlanForUserException(_userId);
        }

        if (!plan.Participants.Any(x => x.ParticipantId == _userId))
        {
            throw new UserDoesNotParticipateInPlanException(_userId, plan.Id);
        }

        return AsPlanWithPointsDto(plan);
    }

    private static PlanWithPointsDTO AsPlanWithPointsDto(Plan plan)
    {
        return new PlanWithPointsDTO()
        {
            Id = plan.Id,
            OwnerId = plan.OwnerId,
            Participants = plan.Participants.Select(x => x.ParticipantId).ToList(),
            Title = plan.Title,
            Description = plan.Description,
            From = plan.From,
            To = plan.To,
            AdditionalCostsValue = plan.AdditionalCostsValue.Amount,
            TotalCostValue = plan.TotalCostValue.Amount,
            TravelPlanPoints = plan.TravelPlanPoints.Select(AsPointDto).ToList(),
            PlanStatus = plan.PlanStatus,
        };
    }

    private static PointDTO AsPointDto(TravelPoint point)
    {
        return new PointDTO()
        {
            Id = point.Id,
            PlaceName = point.PlaceName,
            TotalCost = point.TotalCost.Amount,
            TravelPlanOrderNumber = point.TravelPlanOrderNumber
        };
    }
}