using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.Payments.Application;
using TravelCompanion.Modules.Payments.Domain;
using TravelCompanion.Modules.Payments.Infrastructure;
using TravelCompanion.Shared.Abstractions.Modules;

namespace TravelCompanion.Modules.Payments.Api;

internal class PaymentsModule : IModule
{
    public const string BasePath = "payments-module";
    public string Name { get; } = "Payments";
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