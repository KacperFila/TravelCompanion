using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.Emails.Api.Endpoints.SendEmail;

[Route(EmailsModule.BasePath)]
public class SendEmailEndpoint : EndpointBaseAsync
    .WithRequest<SendEmailRequest>
    .WithActionResult
{
    private readonly IEmailSender _emailSender;

    public SendEmailEndpoint(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost]
    public override async Task<ActionResult> HandleAsync(SendEmailRequest request, CancellationToken cancellationToken = default)
    {
        await _emailSender.SendEmailAsync(Email.Create(request.Subject, request.Body), request.Receiver);
        return Ok();
    }
}