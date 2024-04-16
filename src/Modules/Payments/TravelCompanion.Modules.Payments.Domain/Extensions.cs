using Microsoft.Extensions.DependencyInjection;

namespace TravelCompanion.Modules.Payments.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}