using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.Payments.Domain.Payments.Repositories;
using TravelCompanion.Modules.Payments.Infrastructure.EF.Repositories;
using TravelCompanion.Shared.Infrastructure.Postgres;

namespace TravelCompanion.Modules.Payments.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<PaymentsDbContext>();
        services.AddScoped<ITravelSummaryRepository, TravelSummaryRepository>();

        return services;
    }
}