using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public sealed class CreateTravelPointHandler : ICommandHandler<CreateTravelPoint>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public CreateTravelPointHandler(IPlanRepository planRepository, IContext context)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
    }


    public async Task HandleAsync(CreateTravelPoint command)
    {
        var plan = await _planRepository.GetAsync(command.travelPlanId);
        if (plan is null)
        {
            throw new PlanNotFoundException(command.travelPlanId);
        }

        var isTravelPointAccepted = _userId == plan.OwnerId;

        var travelPoint = TravelPoint.Create(Guid.NewGuid(), command.PlaceName, command.travelPlanId, isTravelPointAccepted);
        
        plan.AddTravelPoint(travelPoint);
        await _planRepository.UpdateAsync(plan);
    }
}