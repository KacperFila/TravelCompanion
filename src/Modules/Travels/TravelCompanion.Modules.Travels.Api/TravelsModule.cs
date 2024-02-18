using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Abstractions.Modules;

namespace TravelCompanion.Modules.Travels.Api;

public class TravelsModule : IModule
{
    public const string BasePath = "travels-module";
    public string Name { get; } = "Travels";
    public string Path => BasePath;
    public IEnumerable<string> Policies { get; } = new [] { "travels" };
    public void Register(IServiceCollection services)
    {
        
    }

    public void Use(IApplicationBuilder app)
    {
        
    }
}