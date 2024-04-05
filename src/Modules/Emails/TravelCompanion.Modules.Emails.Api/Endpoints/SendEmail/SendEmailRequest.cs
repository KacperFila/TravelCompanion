using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.Emails.Api.Endpoints.SendEmail;

public class SendEmailRequest
{
    public Email Email { get; set; }
    public List<string> Receivers { get; set; }
}