using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public sealed class CreateTravelPlanHandler : ICommandHandler<CreateTravelPlan>
{
    private readonly IPlanRepository _planRepository;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly IContext _context;
    private readonly Guid _userId;

    public CreateTravelPlanHandler(IPlanRepository planRepository, IContext context, IUsersModuleApi usersModuleApi)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _usersModuleApi = usersModuleApi;
    }

    public async Task HandleAsync(CreateTravelPlan command)
    {
        var travelPlan = Plan.Create(
            _userId,
            command.title,
            command.description,
            command.from,
            command.to);

        await _planRepository.AddAsync(travelPlan);
    }
}