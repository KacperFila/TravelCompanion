using System;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Infrastructure.Postgres;

namespace TravelCompanion.Shared.Infrastructure.BackgroundJobs;

internal static class Extensions
{
    internal const string DashboardPath = "/hangfire";

    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        //var hangfireConnectionString = configuration.GetConnectionString("postgres:hangfireConnectionString");

        var options = services.GetOptions<PostgresOptions>("postgres");

        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(opt => opt
                    .UseNpgsqlConnection(options.HangfireConnectionString)));

        services.AddHangfireServer();

        return services;
    }


    internal static void UseBackgroundJobs(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard(DashboardPath);

        RecurringJob.AddOrUpdate(() => Console.WriteLine("HELLO FROM HANGFIRE"), "* * * * *");
    }
}