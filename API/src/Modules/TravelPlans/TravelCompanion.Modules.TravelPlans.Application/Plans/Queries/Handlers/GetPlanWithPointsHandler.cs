using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

public sealed class GetPlanWithPointsHandler : IQueryHandler<GetPlanWithPoints, PlanWithPointsDTO>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public GetPlanWithPointsHandler(IPlanRepository planRepository, IContext context)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task<PlanWithPointsDTO> HandleAsync(GetPlanWithPoints query)
    {
        var plan = await _planRepository.GetAsync(query.planId);

        if (plan is null)
        {
            throw new PlanNotFoundException(query.planId);
        }

        if (!plan.Participants.Contains(_userId))
        {
            throw new UserDoesNotParticipateInPlanException(_userId, query.planId);
        }

        return AsPlanWithPointsDto(plan);
    }

    private static PlanWithPointsDTO AsPlanWithPointsDto(Plan plan)
    {
        return new PlanWithPointsDTO()
        {
            Id = plan.Id,
            OwnerId = plan.OwnerId,
            Participants = plan.Participants.Select(x => x.Value).ToList(),
            Title = plan.Title,
            Description = plan.Description,
            From = plan.From,
            To = plan.To,
            AdditionalCostsValue = plan.AdditionalCostsValue.Amount,
            TotalCostValue = plan.TotalCostValue.Amount,
            PlanPoints = plan.TravelPlanPoints.Select(AsPointDto).ToList(),
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
        };
    }
}