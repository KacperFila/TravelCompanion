using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Emails.Core.Entities;
using TravelCompanion.Modules.Emails.Core.Services;

namespace TravelCompanion.Modules.Emails.Api.Endpoints;

[Route(EmailsModule.BasePath)]
public class SendEmailEndpoint : EndpointBaseAsync
    .WithRequest<Email>
    .WithActionResult
{
    private readonly IEmailSender _emailSender;

    public SendEmailEndpoint(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost]
    public override async Task<ActionResult> HandleAsync(Email request, CancellationToken cancellationToken = new CancellationToken())
    {
        await _emailSender.SendEmailAsync(request);
        return Ok();
    }
}