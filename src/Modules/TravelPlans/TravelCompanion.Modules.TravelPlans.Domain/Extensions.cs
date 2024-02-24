using Microsoft.Extensions.DependencyInjection;

namespace TravelCompanion.Modules.TravelPlans.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}