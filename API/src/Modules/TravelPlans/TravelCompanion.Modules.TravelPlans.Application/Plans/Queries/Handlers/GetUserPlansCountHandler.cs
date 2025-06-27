using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

internal sealed class GetUserPlansCountHandler : IQueryHandler<GetUserPlansCount, int>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public GetUserPlansCountHandler(
        IPlanRepository planRepository,
        IContext context)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task<int> HandleAsync(GetUserPlansCount query)
    {
        return await _planRepository.GetPlanCountAsync(_userId);
    }
}