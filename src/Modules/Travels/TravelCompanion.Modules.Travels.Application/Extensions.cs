using Microsoft.Extensions.DependencyInjection;

namespace TravelCompanion.Modules.Travels.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}