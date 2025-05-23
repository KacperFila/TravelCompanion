using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.Travels.Core.DTO;

public class AcceptedPlanEmailDto : Email
{
    public AcceptedPlanEmailDto()
    {
        Subject = "Plan Acceptance!";
        Body = "We would like to inform you that one of your plans has been updated! See your account for more info.";
    }
}