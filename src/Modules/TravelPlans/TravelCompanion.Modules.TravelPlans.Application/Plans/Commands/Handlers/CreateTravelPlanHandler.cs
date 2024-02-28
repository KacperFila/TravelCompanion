using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public sealed class CreateTravelPlanHandler : ICommandHandler<CreateTravelPlan>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;

    public CreateTravelPlanHandler(IPlanRepository planRepository, IContext context)
    {
        _planRepository = planRepository;
        _context = context;
    }

    public async Task HandleAsync(CreateTravelPlan command)
    {
        var travelPlan = Plan.Create(command.Id ,_context.Identity.Id, command.title, command.description,
            command.from, command.to);

        await _planRepository.AddAsync(travelPlan);
    }
}