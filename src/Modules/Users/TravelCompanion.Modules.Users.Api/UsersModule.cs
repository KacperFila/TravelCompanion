using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.Users.Core;
using TravelCompanion.Shared.Abstractions.Modules;

namespace TravelCompanion.Modules.Users.Api;

internal sealed class UsersModule : IModule
{
	public const string BasePath = "users-module";
	public string Name { get; } = "Users";
	public string Path => BasePath;

	public IEnumerable<string> Policies { get; } = new[]
	{
		"users"
	};

	public void Register(IServiceCollection services)
	{
		services.AddCore();
	}

	public void Use(IApplicationBuilder app)
	{
	}
}