namespace TravelCompanion.Shared.Infrastructure.Emails;

public class EmailOptions
{
    public string From { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}