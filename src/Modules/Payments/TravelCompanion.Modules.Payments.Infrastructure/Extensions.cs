using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Infrastructure.Postgres;

namespace TravelCompanion.Modules.Payments.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<PaymentsDbContext>();

        return services;
    }
}