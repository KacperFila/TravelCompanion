using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("TravelCompanion.Modules.TravelPlans.Tests.Unit")]
[assembly:InternalsVisibleTo("DynamicProxyAssemblyGen2")]
namespace TravelCompanion.Modules.TravelPlans.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}