using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.Emails.Core;
using TravelCompanion.Shared.Abstractions.Modules;

namespace TravelCompanion.Modules.Emails.Api;

internal class EmailsModule : IModule
{
    public const string BasePath = "emails-module";
    public string Name { get; } = "Emails";
    public string Path => BasePath;
    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {

    }
}