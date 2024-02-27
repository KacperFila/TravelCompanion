using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;
using TravelCompanion.Shared.Infrastructure.Postgres;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITravelPlanRepository, TravelPlanRepository>();
        services.AddScoped<ITravelPlanInvitationRepository, TravelPlanInvitationRepository>();
        services.AddScoped<ITravelPointRepository, TravelPointRepository>();
        services.AddPostgres<TravelPlansDbContext>();

        return services;
    }
}