using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.Travels.Core.Services;

[assembly: InternalsVisibleTo("TravelCompanion.Modules.Travels.Api")]
namespace TravelCompanion.Modules.Travels.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<ITravelService, TravelService>();
        return services;
    }
}