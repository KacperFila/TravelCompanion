using System;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using TravelCompanion.Shared.Abstractions.Emails;
namespace TravelCompanion.Shared.Infrastructure.Emails;

internal sealed class EmailSender : IEmailSender
{
    private readonly EmailOptions _emailOptions;

    public EmailSender(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendEmailAsync(Email email, List<string> receiversEmails)
    {
        var mimeEmail = new MimeMessage();

        mimeEmail.From.Add(MailboxAddress.Parse(_emailOptions.From));
        var addresses = receiversEmails.Select(MailboxAddress.Parse).ToList();
        mimeEmail.To.AddRange(addresses);
        mimeEmail.Subject = email.Subject;
        mimeEmail.Body = new TextPart(TextFormat.Html) { Text = email.Body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_emailOptions.Host, 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailOptions.Username, _emailOptions.Password);
        await smtp.SendAsync(mimeEmail);
        await smtp.DisconnectAsync(true);
    }

    public async Task SendEmailAsync(Email email, string receiverEmail)
    {
        var mimeEmail = new MimeMessage();

        mimeEmail.From.Add(MailboxAddress.Parse(_emailOptions.From));
        mimeEmail.To.Add(MailboxAddress.Parse(receiverEmail));
        mimeEmail.Subject = email.Subject;
        mimeEmail.Body = new TextPart(TextFormat.Html) { Text = email.Body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_emailOptions.Host, 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailOptions.Username, _emailOptions.Password);
        await smtp.SendAsync(mimeEmail);
        await smtp.DisconnectAsync(true);
    }
}