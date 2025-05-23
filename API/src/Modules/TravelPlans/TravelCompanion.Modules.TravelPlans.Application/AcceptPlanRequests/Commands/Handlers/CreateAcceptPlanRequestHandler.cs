using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.AcceptPlanRequests.Commands.Handlers;

public class CreateAcceptPlanRequestHandler : ICommandHandler<CreateAcceptPlanRequest>
{
    private readonly IPlanAcceptRequestRepository _planAcceptRequestRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;
    public CreateAcceptPlanRequestHandler(IPlanAcceptRequestRepository planAcceptRequestRepository, IContext context, IPlanRepository planRepository)
    {
        _planAcceptRequestRepository = planAcceptRequestRepository;
        _context = context;
        _planRepository = planRepository;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(CreateAcceptPlanRequest command)
    {
        var doesRequestExist = await _planAcceptRequestRepository.ExistsByPlanAsync(command.PlanId);

        if (doesRequestExist)
        {
            throw new CreateAcceptPlanRequestAlreadyExistsException(command.PlanId);
        }

        var plan = await _planRepository.GetAsync(command.PlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(command.PlanId);
        }

        if (plan.OwnerId != _userId)
        {
            throw new UserNotAllowedToChangePlanException(command.PlanId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        var request = PlanAcceptRequest.Create(command.PlanId);
        plan.ChangeStatusToDuringAcceptance();
        await _planAcceptRequestRepository.AddAsync(request);
    }
}