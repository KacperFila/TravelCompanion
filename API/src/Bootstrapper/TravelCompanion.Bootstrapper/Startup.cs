namespace TravelCompanion.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TravelCompanion.Shared.Abstractions.Modules;
using TravelCompanion.Shared.Infrastructure;
using TravelCompanion.Shared.Infrastructure.Modules;

public class Startup
{
    private readonly IList<IModule> _modules;
    private readonly IList<Assembly> _assemblies;

    public Startup(IConfiguration configuration)
    {
        _assemblies = ModuleLoader.LoadAssemblies(configuration);
        _modules = ModuleLoader.LoadModules(_assemblies);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructure(_assemblies, _modules);

        foreach (var module in _modules)
        {
            module.Register(services);
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        app.UseInfrastructure();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("TravelCompanion API!");
            });
            endpoints.MapModuleInfo();
        });

        foreach (var module in _modules)
        {
            module.Use(app);
        }

        logger.LogInformation($"Modules: {string.Join(", ", _modules.Select(x => x.Name))}");

        _assemblies.Clear();
        _modules.Clear();
    }
}