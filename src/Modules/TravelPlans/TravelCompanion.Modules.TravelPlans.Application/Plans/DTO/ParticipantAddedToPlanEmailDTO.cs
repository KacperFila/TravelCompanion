using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;

public class ParticipantAddedToPlanEmailDTO : Email
{
    public ParticipantAddedToPlanEmailDTO(string planTitle)
    {
        Subject = "You've been added to plan!";
        Body = $"You've been added to plan: {planTitle}";
    }
}