namespace TravelCompanion.Modules.Emails.Api.Endpoints.SendEmail;

public class SendEmailRequest
{
    public string Receiver { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}