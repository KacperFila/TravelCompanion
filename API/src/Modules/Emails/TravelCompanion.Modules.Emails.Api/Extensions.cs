using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using TravelCompanion.Modules.Emails.Core;

[assembly: InternalsVisibleTo("TravelCompanion.Bootstrapper")]
namespace TravelCompanion.Modules.Emails.Api;

internal static class Extensions
{
    public static IServiceCollection AddEmails(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }
}