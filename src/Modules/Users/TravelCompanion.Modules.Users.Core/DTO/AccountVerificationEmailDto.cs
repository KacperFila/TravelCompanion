using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.Users.Core.DTO;

public class AccountVerificationEmailDTO : Email
{
    public AccountVerificationEmailDTO(string activationLink)
    {
        Subject = "Verify Your account!";
        Body = $"To finish activation process please click in the following link: {activationLink}.";
    }
}