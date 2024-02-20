using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.Travels.Core;

[assembly: InternalsVisibleTo("TravelCompanion.Bootstrapper")]
namespace TravelCompanion.Modules.Travels.Api;

internal static class Extensions
{
    public static IServiceCollection AddTravels(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }

}