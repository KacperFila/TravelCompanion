using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TravelCompanion.Shared.Abstractions.Modules;
using TravelCompanion.Shared.Abstractions.Storage;
using TravelCompanion.Shared.Abstractions.Time;
using TravelCompanion.Shared.Infrastructure.Api;
using TravelCompanion.Shared.Infrastructure.Auth;
using TravelCompanion.Shared.Infrastructure.Commands;
using TravelCompanion.Shared.Infrastructure.Contexts;
using TravelCompanion.Shared.Infrastructure.Events;
using TravelCompanion.Shared.Infrastructure.Exceptions;
using TravelCompanion.Shared.Infrastructure.Kernel;
using TravelCompanion.Shared.Infrastructure.Messaging;
using TravelCompanion.Shared.Infrastructure.Modules;
using TravelCompanion.Shared.Infrastructure.Postgres;
using TravelCompanion.Shared.Infrastructure.Queries;
using TravelCompanion.Shared.Infrastructure.Services;
using TravelCompanion.Shared.Infrastructure.Storage;
using TravelCompanion.Shared.Infrastructure.Time;

[assembly: InternalsVisibleTo("TravelCompanion.Bootstrapper")]
[assembly: InternalsVisibleTo("TravelCompanion.Services.Tickets.Core")]
[assembly: InternalsVisibleTo("TravelCompanion.Shared.Tests")]
namespace TravelCompanion.Shared.Infrastructure;

internal static class Extensions
{
	private const string CorsPolicy = "cors";

	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		IList<Assembly> assemblies, IList<IModule> modules)
	{
		var disabledModules = new List<string>();
		using (var serviceProvider = services.BuildServiceProvider())
		{
			var configuration = serviceProvider.GetRequiredService<IConfiguration>();
			foreach (var (key, value) in configuration.AsEnumerable())
			{
				if (!key.Contains(":module:enabled"))
				{
					continue;
				}

				if (!bool.Parse(value))
				{
					disabledModules.Add(key.Split(":")[0]);
				}
			}
		}

		services.AddCors(cors =>
		{
			cors.AddPolicy(CorsPolicy, x =>
			{
				x.WithOrigins("*")
					.WithMethods("POST", "PUT", "DELETE")
					.WithHeaders("Content-Type", "Authorization");
			});
		});
		services.AddSwaggerGen(swagger =>
		{
			swagger.CustomSchemaIds(x => x.FullName);
			swagger.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "TravelCompanion API",
				Version = "v1"
			});
		});

		services.AddMemoryCache();
		services.AddSingleton<IRequestStorage, RequestStorage>();
		services.AddSingleton<IContextFactory, ContextFactory>();
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
		services.AddModuleInfo(modules);
		services.AddModuleRequests(assemblies);
		services.AddAuth(modules);
		services.AddErrorHandling();
		services.AddCommands(assemblies);
		services.AddQueries(assemblies);
		services.AddEvents(assemblies);
		services.AddDomainEvents(assemblies);
		services.AddMessaging();
		services.AddPostgres();
		services.AddTransactionalDecorators();
		services.AddSingleton<IClock, UtcClock>();
		services.AddHostedService<AppInitializer>();
		services.AddControllers()
			.ConfigureApplicationPartManager(manager =>
			{
				var removedParts = new List<ApplicationPart>();
				foreach (var disabledModule in disabledModules)
				{
					var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
						StringComparison.InvariantCultureIgnoreCase));
					removedParts.AddRange(parts);
				}

				foreach (var part in removedParts)
				{
					manager.ApplicationParts.Remove(part);
				}

				manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
			});

		return services;
	}

	public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
	{
		app.UseCors(CorsPolicy);
		app.UseErrorHandling();
		app.UseSwagger();
		app.UseReDoc(reDoc =>
		{
			reDoc.RoutePrefix = "docs";
			reDoc.SpecUrl("/swagger/v1/swagger.json");
			reDoc.DocumentTitle = "TravelCompanion API";
		});
		app.UseAuthentication();
		app.UseRouting();
		app.UseAuthorization();

		return app;
	}

	public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
	{
		using var serviceProvider = services.BuildServiceProvider();
		var configuration = serviceProvider.GetRequiredService<IConfiguration>();
		return configuration.GetOptions<T>(sectionName);
	}

	public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
	{
		var options = new T();
		configuration.GetSection(sectionName).Bind(options);
		return options;
	}

	public static string GetModuleName(this object value)
		=> value?.GetType().GetModuleName() ?? string.Empty;

	public static string GetModuleName(this Type type)
	{
		if (type?.Namespace is null)
		{
			return string.Empty;
		}

		return type.Namespace.StartsWith("TravelCompanion.Modules.")
			? type.Namespace.Split(".")[2].ToLowerInvariant()
			: string.Empty;
	}
}
