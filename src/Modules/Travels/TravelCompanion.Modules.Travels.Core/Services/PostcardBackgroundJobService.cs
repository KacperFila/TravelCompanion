using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Shared.Abstractions.BackgroundJobs;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal sealed class PostcardBackgroundJobService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private const string jobId = "RemoveUnapprovedPostcards";
    public PostcardBackgroundJobService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var scheduler = scope.ServiceProvider.GetRequiredService<IBackgroundJobScheduler>();
        var postcardRepository = scope.ServiceProvider.GetRequiredService<IPostcardRepository>();

        scheduler.ScheduleDaily(() => postcardRepository.DeleteExpiredUnapprovedAsync(), jobId);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var backgroundJobScheduler = scope.ServiceProvider.GetRequiredService<IBackgroundJobScheduler>();
        backgroundJobScheduler.RemoveIfExists(jobId);
    }
}