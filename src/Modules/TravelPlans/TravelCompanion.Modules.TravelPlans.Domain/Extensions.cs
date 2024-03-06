using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;

namespace TravelCompanion.Modules.TravelPlans.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IPlansDomainService, PlansDomainService>();
        services.AddScoped<ITravelPointDomainService, TravelPointDomainService>();

        return services;
    }
}