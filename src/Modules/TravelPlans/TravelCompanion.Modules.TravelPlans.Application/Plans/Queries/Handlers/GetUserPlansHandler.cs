using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

public sealed class GetUserPlansHandler : IQueryHandler<GetUserPlans, List<PlanWithPointsDto>>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public GetUserPlansHandler(IPlanRepository planRepository, IContext context)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task<List<PlanWithPointsDto>> HandleAsync(GetUserPlans query)
    {
        var plans = await _planRepository.BrowseForUserAsync(_userId);
        var dtos = plans.Select(AsPlanWithPointsDto).ToList();

        return dtos;
    }

    private static PlanWithPointsDto AsPlanWithPointsDto(Plan plan)
    {
        return new PlanWithPointsDto()
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

    private static PointDto AsPointDto(TravelPoint point)
    {
        return new PointDto()
        {
            Id = point.Id,
            PlaceName = point.PlaceName,
            TotalCost = point.TotalCost.Amount,
        };
    }
}