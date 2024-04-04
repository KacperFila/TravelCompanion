using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using TravelCompanion.Modules.Emails.Core.Entities;
using TravelCompanion.Shared.Infrastructure.Emails;

namespace TravelCompanion.Modules.Emails.Core.Services;

internal sealed class EmailSender : IEmailSender
{
    private readonly EmailOptions _emailOptions;

    public EmailSender(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendEmailAsync(Email email)
    {
        var mimeEmail = new MimeMessage();

        mimeEmail.From.Add(MailboxAddress.Parse(_emailOptions.From));
        mimeEmail.To.Add(MailboxAddress.Parse(email.To)); 
        mimeEmail.Subject = email.Subject;
        mimeEmail.Body = new TextPart(TextFormat.Html) { Text = email.Body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_emailOptions.Host, 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailOptions.Username, _emailOptions.Password);
        await smtp.SendAsync(mimeEmail);
        await smtp.DisconnectAsync(true);

        return;
    }
}