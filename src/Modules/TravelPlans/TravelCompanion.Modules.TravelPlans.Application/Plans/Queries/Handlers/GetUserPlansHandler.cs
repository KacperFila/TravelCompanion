using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

public sealed class GetUserPlansHandler : IQueryHandler<GetUserPlans, Paged<PlanDetailsDTO>>
{
    private readonly IPlanRepository _planRepository;

    public GetUserPlansHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<Paged<PlanDetailsDTO>> HandleAsync(GetUserPlans query)
    {
        var plans = await _planRepository.BrowseAsync(query.Page, query.Results);

        var dtos = plans.Items.Select(AsPlanDetailsDto).ToList();
        //TODO finish pagination
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