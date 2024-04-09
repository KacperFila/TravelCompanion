using System.Globalization;
using TravelCompanion.Modules.Emails.Core.DTO;
using TravelCompanion.Modules.Travels.Shared;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Emails;
using TravelCompanion.Shared.Abstractions.Time;

namespace TravelCompanion.Modules.Emails.Core.Commands.Handlers;

internal sealed class SendThrowbackEmailsHandler : ICommandHandler<SendThrowbackEmails>
{
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly ITravelsModuleApi _travelsModuleApi;
    private readonly IEmailSender _emailSender;
    private readonly IClock _clock;
    public SendThrowbackEmailsHandler(IUsersModuleApi usersModuleApi, ITravelsModuleApi travelsModuleApi, IEmailSender emailSender, IClock clock)
    {
        _usersModuleApi = usersModuleApi;
        _travelsModuleApi = travelsModuleApi;
        _emailSender = emailSender;
        _clock = clock;
    }

    public async Task HandleAsync(SendThrowbackEmails command)
    {
        var today = _clock.CurrentDate();
        var usersIds = await _usersModuleApi.GetUsersIdsAsync();
  
        foreach (var userId in usersIds)
        {
            var postcards = await _travelsModuleApi.GetUserLastYearPostcardsFromMonth(userId, today.Month);

            if (postcards.Any())
            {
                var userEmail = await _usersModuleApi.GetUserEmail(userId);
                await _emailSender.SendEmailAsync(
                    new ThrowbackEmailDto(today.ToString("MMMM", CultureInfo.InvariantCulture), postcards),
                    userEmail);
            }
        }
    }
}