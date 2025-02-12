using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Abstractions.BackgroundJobs;
using TravelCompanion.Shared.Infrastructure.Postgres;

namespace TravelCompanion.Shared.Infrastructure.BackgroundJobs;

internal static class Extensions
{
    internal const string DashboardPath = "/hangfire";
    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
       
        var options = services.GetOptions<PostgresOptions>("postgres");

        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(opt => opt
                    .UseNpgsqlConnection(options.HangfireConnectionString)));

        services.AddHangfireServer();
        services.AddScoped<IBackgroundJobScheduler, BackgroundJobScheduler>();

        return services;
    }

    internal static void UseBackgroundJobs(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard(DashboardPath, new DashboardOptions
        {
            Authorization = new[] { new AllowAllAuthorizationFilter() }
        });
    }

    private class AllowAllAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}