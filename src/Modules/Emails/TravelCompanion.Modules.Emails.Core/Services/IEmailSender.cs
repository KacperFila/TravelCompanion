using TravelCompanion.Modules.Emails.Core.Entities;

namespace TravelCompanion.Modules.Emails.Core.Services;

public interface IEmailSender
{
    public Task SendEmailAsync(Email email);
}