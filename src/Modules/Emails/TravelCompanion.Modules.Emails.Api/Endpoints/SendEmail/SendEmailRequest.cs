using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.Emails.Api.Endpoints.SendEmail;

public class SendEmailRequest
{
    public string Email { get; set; }
    public List<string> Receivers { get; set; }
}