namespace TravelCompanion.Shared.Abstractions.Emails;

public abstract class Email
{
    public string Subject { get; set; }
    public string Body { get; set; }
}