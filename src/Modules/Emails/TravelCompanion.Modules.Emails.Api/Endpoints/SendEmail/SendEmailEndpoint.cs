using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TravelCompanion.Shared.Abstractions.Emails;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Infrastructure.Notifications;

namespace TravelCompanion.Modules.Emails.Api.Endpoints.SendEmail;

[Route(EmailsModule.BasePath)]
public class SendEmailEndpoint : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult
{
    private readonly IHubContext<NotificationsHub, INotificationClient> _notificationsHubContext;
    private readonly INotificationService _notificationService;

    public SendEmailEndpoint(IEmailSender emailSender, IHubContext<NotificationsHub, INotificationClient> notificationsHubContext, INotificationService notificationService)
    {
        _notificationsHubContext = notificationsHubContext;
        _notificationService = notificationService;
    }

    [HttpPost]
    public override async Task<ActionResult> HandleAsync(string request, CancellationToken cancellationToken = default)
    {
        await _notificationService.SendToAsync("e1636219-8816-4eab-b027-11548e588f74", "To jest z serwisu message!!!");
        return Ok();
    }
}