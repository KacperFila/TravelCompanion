using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

public sealed class GetUserPlansHandler : IQueryHandler<GetUserPlans, Paged<PlanDetailsDTO>>
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

    public async Task<Paged<PlanDetailsDTO>> HandleAsync(GetUserPlans query)
    {
        var plans = await _planRepository.BrowseForUserAsync(_userId, query.Page, query.Results);
        var dtos = plans.Items.Select(AsPlanDetailsDto).ToList();

        var pagedDtos = new Paged<PlanDetailsDTO>(dtos, plans.CurrentPage, plans.ResultsPerPage, plans.TotalPages, plans.TotalResults);

        return pagedDtos;
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