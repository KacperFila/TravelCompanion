using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
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
        await _emailSender.SendEmailAsync(request.Email, request.Receivers);
        return Ok();
    }
}