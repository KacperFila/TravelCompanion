using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.Users.Core.DTO;

public class AccountVerificationEmailDto : Email
{
    public AccountVerificationEmailDto(string activationLink)
    {
        Subject = "Verify Your account!";
        Body = $"To finish activation process please click in the following link: {activationLink}.";
    }
}