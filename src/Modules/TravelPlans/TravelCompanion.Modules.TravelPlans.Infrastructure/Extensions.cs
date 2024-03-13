using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;
using TravelCompanion.Modules.TravelPlans.Infrastructure.Services;
using TravelCompanion.Modules.TravelPlans.Shared;
using TravelCompanion.Shared.Infrastructure.Postgres;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IInvitationRepository, InvitationRepository>();
        services.AddScoped<ITravelPointRepository, TravelPointRepository>();
        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        services.AddScoped<ITravelPointUpdateRequestRepository, TravelPointUpdateRequestRepository>();
        services.AddScoped<ITravelPointRemoveRequestRepository, TravelPointRemoveRequestRepository>();
        services.AddPostgres<TravelPlansDbContext>();
        services.AddTransient<ITravelPlansModuleApi, TravelPlansModuleApi>();

        return services;
    }
}