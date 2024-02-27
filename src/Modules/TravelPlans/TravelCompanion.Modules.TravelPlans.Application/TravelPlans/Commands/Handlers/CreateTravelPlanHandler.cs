using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlans.Commands.Handlers;

public sealed class CreateTravelPlanHandler : ICommandHandler<CreateTravelPlan>
{
    private readonly ITravelPlanRepository _travelPlanRepository;
    private readonly IContext _context;

    public CreateTravelPlanHandler(ITravelPlanRepository travelPlanRepository, IContext context)
    {
        _travelPlanRepository = travelPlanRepository;
        _context = context;
    }

    public async Task HandleAsync(CreateTravelPlan command)
    {
        var travelPlan = TravelPlan.Create(command.Id ,_context.Identity.Id, command.title, command.description,
            command.from, command.to);

        await _travelPlanRepository.AddAsync(travelPlan);
    }
}