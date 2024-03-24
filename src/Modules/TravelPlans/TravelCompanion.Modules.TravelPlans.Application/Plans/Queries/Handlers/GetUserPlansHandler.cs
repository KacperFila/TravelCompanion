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
        var plans = await _planRepository.BrowseAsync();
    }
}