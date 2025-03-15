using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public sealed class CreateTravelPointHandler : ICommandHandler<CreateTravelPoint>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;
    private readonly INotificationRealTimeService _notificationService;

    public CreateTravelPointHandler(
        IPlanRepository planRepository,
        IContext context,
        INotificationRealTimeService notificationService)
    {
        _planRepository = planRepository;
        _context = context;
        _notificationService = notificationService;
        _userId = _context.Identity.Id;
    }


    public async Task HandleAsync(CreateTravelPoint command)
    {
        var plan = await _planRepository.GetAsync(command.travelPlanId);
        
        if (plan is null)
        {
            throw new PlanNotFoundException(command.travelPlanId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        var isPointAccepted = _userId == plan.OwnerId;
        var newPointNumber = GetNewTravelPointNumber(plan);

        var point = TravelPoint.Create(Guid.NewGuid(), command.placeName, command.travelPlanId, isPointAccepted, newPointNumber);
        
        plan.AddTravelPoint(point);
        await _planRepository.UpdateAsync(plan);

        var notification =
            NotificationMessage.Create(point.PlaceName, "One of your suggested changes has been accepted!");
        await _notificationService.SendToAsync(plan.OwnerId.ToString(), notification);
    }

    private int GetNewTravelPointNumber(Plan plan)
    {
        return plan.TravelPlanPoints.Count + 1;
    }
}