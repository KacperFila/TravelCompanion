using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

public sealed class GetUserPlansHandler : IQueryHandler<GetUserPlans, Paged<PlanWithPointsDTO>>
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

    public async Task<Paged<PlanWithPointsDTO>> HandleAsync(GetUserPlans query)
    {
        var plans = await _planRepository.BrowseForUserAsync(_userId, query.Page, query.Results, query.SortOrder, query.OrderBy);
        var dtos = plans.Items.Select(AsPlanWithPointsDto).ToList();

        var pagedDtos = new Paged<PlanWithPointsDTO>(dtos, plans.CurrentPage, plans.ResultsPerPage, plans.TotalPages, plans.TotalResults);

        return pagedDtos;
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