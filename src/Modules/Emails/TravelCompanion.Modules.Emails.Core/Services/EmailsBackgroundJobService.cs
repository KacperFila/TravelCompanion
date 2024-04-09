﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TravelCompanion.Modules.Emails.Core.Commands;
using TravelCompanion.Shared.Abstractions.BackgroundJobs;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.Emails.Core.Services;

internal sealed class EmailsBackgroundJobService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public EmailsBackgroundJobService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var scheduler = scope.ServiceProvider.GetRequiredService<IBackgroundJobScheduler>();
            var commandDispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();

            scheduler.ScheduleMonthly(() => commandDispatcher.SendAsync(new SendThrowbackEmails()));
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
    }
}