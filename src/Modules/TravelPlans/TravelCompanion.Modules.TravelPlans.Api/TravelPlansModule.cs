using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.TravelPlans.Application;
using TravelCompanion.Modules.TravelPlans.Domain;
using TravelCompanion.Modules.TravelPlans.Infrastructure;
using TravelCompanion.Shared.Abstractions.Modules;

namespace TravelCompanion.Modules.TravelPlans.Api;

internal class TravelPlansModule : IModule
{
    public const string BasePath = "travelplans-module";
    public string Name { get; } = "TravelPlans";
    public string Path => BasePath;
    public void Register(IServiceCollection services)
    {
        services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}