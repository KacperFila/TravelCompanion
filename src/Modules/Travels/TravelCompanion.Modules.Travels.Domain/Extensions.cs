using Microsoft.Extensions.DependencyInjection;

namespace TravelCompanion.Modules.Travels.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}