using Microsoft.Extensions.DependencyInjection;

namespace TravelCompanion.Modules.TravelPlans.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}