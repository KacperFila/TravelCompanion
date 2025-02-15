namespace TravelCompanion.Shared.Abstractions.Emails;

public class Email
{
    public string Subject { get; set; }
    public string Body { get; set; }

    public static Email Create(string subject, string body)
    {
        return new Email
        {
            Body = body,
            Subject = subject
        };
    }
}