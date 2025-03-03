using NSubstitute;
using Shouldly;
using TravelCompanion.Modules.TravelPlans.Application.Plans.Events.External;
using TravelCompanion.Modules.TravelPlans.Application.Plans.Events.External.Handlers;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Events.Handlers;

public class AcceptPlanRequestParticipantAddedHandlerTests
{
    private Task Act(AcceptPlanRequestParticipantAdded @event) => _handler.HandleAsync(@event);

    [Fact]
    public async Task given_plan_was_not_found_change_status_should_not_occur()
    {
        var plan = GetPlan(_participantId);
        var @event = new AcceptPlanRequestParticipantAdded(_participantId, plan.Id);
        
        var exception = await Record.ExceptionAsync(() => Act(@event));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PlanNotFoundException>();
    }

    [Fact]
    public async Task given_request_was_not_found_change_status_should_not_occur()
    {
        var plan = GetPlan(_participantId);
        var @event = new AcceptPlanRequestParticipantAdded(_participantId, plan.Id);
        _planRepository.GetAsync(@event.planId).Returns(plan);

        var exception = await Record.ExceptionAsync(() => Act(@event));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AcceptPlanRequestForPlanNotFoundException>();
    }

    [Fact]
    public async Task given_user_acceptance_was_not_final_plan_status_change_should_not_occur()
    {
        var plan = GetPlanWithMultipleParticipants(_participantId);
        var @event = new AcceptPlanRequestParticipantAdded(_participantId, plan.Id);
        var request = GetPlanAcceptRequest(plan.Id, _participantId);

        _planRepository.GetAsync(@event.planId).Returns(plan);
        _planAcceptRequestRepository.GetByPlanAsync(plan.Id).Returns(request);

        var exception = await Record.ExceptionAsync(() => Act(@event));

        exception.ShouldBeNull();
    }

    [Fact]
    public async Task given_user_acceptance_was_final_plan_status_change_should_occur()
    {
        var plan = GetPlan(_participantId);
        var @event = new AcceptPlanRequestParticipantAdded(_participantId, plan.Id);
        var request = GetPlanAcceptRequest(plan.Id, _participantId);

        _planRepository.GetAsync(@event.planId).Returns(plan);
        _planAcceptRequestRepository.GetByPlanAsync(plan.Id).Returns(request);

        var exception = await Record.ExceptionAsync(() => Act(@event));

        exception.ShouldBeNull();
        plan.PlanStatus.ShouldBe(PlanStatus.Accepted);
    }

    private readonly IEventHandler<AcceptPlanRequestParticipantAdded> _handler;
    private readonly IPlanRepository _planRepository;
    private readonly IPlanAcceptRequestRepository _planAcceptRequestRepository;
    private readonly Guid _participantId;
    public AcceptPlanRequestParticipantAddedHandlerTests()
    {
        _planRepository = Substitute.For<IPlanRepository>();
        _planAcceptRequestRepository = Substitute.For<IPlanAcceptRequestRepository>();
        _participantId = Guid.NewGuid();
        _handler = new AcceptPlanRequestParticipantAddedHandler(_planRepository, _planAcceptRequestRepository);
    }

    private static Plan GetPlan(Guid participantId)
    {
        var plan =  Plan.Create(participantId, "title", "desc", DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2)),
            DateOnly.FromDateTime(DateTime.UtcNow).AddDays(3));
        
        return plan;
    }

    private static Plan GetPlanWithMultipleParticipants(Guid participantId)
    {
        var plan = GetPlan(participantId);
        Enumerable.Range(0, 5).ToList().ForEach(_ => plan.AddParticipant(Guid.NewGuid()));

        return plan;
    }

    private static PlanAcceptRequest GetPlanAcceptRequest(Guid planId, Guid participantId)
    {
        var request = PlanAcceptRequest.Create(planId);
        request.ParticipantsAccepted.Add(participantId);

        return request;
    }
}