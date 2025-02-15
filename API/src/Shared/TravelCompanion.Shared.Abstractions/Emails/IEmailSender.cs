using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Emails;

public interface IEmailSender
{
    public Task SendEmailAsync(Email email, List<string> receiversEmails);
    public Task SendEmailAsync(Email email, string receiverEmail);
}