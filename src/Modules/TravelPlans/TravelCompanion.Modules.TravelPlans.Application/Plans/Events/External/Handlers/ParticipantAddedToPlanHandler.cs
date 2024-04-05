using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Emails;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Events.External.Handlers;

public sealed class ParticipantAddedToPlanHandler : IEventHandler<ParticipantAddedToPlan>
{
    private readonly IPlanRepository _planRepository;
    private readonly IEmailSender _emailSender;
    private readonly IUsersModuleApi _usersModuleApi;

    public ParticipantAddedToPlanHandler(IPlanRepository planRepository, IEmailSender emailSender, IUsersModuleApi usersModuleApi)
    {
        _planRepository = planRepository;
        _emailSender = emailSender;
        _usersModuleApi = usersModuleApi;
    }

    public async Task HandleAsync(ParticipantAddedToPlan @event)
    {
        var plan = await _planRepository.GetAsync(@event.planId);

        if (plan == null)
        {
            throw new PlanNotFoundException(@event.planId);
        }

        foreach (var planAdditionalCost in plan.AdditionalCosts)
        {
            planAdditionalCost.AddReceiptParticipant(@event.participantId);
        }

        await _planRepository.UpdateAsync(plan);

        var userEmail = await _usersModuleApi.GetUserEmail(@event.participantId);
        await _emailSender.SendEmailAsync(new ParticipantAddedToPlanEmailDTO(plan.Title), userEmail);
    }
}