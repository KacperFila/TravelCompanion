using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using TravelCompanion.Modules.Emails.Core.Services;

[assembly: InternalsVisibleTo("TravelCompanion.Modules.Emails.Api")]
namespace TravelCompanion.Modules.Emails.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        //services.AddHostedService<EmailsBackgroundJobService>();
        return services;
    }
}