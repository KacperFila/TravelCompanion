using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;

public class ParticipantAddedToPlanEmailDto : Email
{
    public ParticipantAddedToPlanEmailDto(string planTitle)
    {
        Subject = "You've been added to plan!";
        Body = $"You've been added to plan: {planTitle}";
    }
}