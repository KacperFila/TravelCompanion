using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using TravelCompanion.Modules.Travels.Core.DAL;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Policies;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;
using TravelCompanion.Modules.Travels.Core.Services;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Modules.Travels.Core.Validators;
using TravelCompanion.Modules.Travels.Shared;
using TravelCompanion.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("TravelCompanion.Modules.Travels.Api")]
namespace TravelCompanion.Modules.Travels.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<TravelsDbContext>();

        services.AddValidatorsFromAssemblyContaining<TravelDtoValidator>();

        services.AddScoped<ITravelService, TravelService>();
        services.AddScoped<IPostcardService, PostcardService>();

        services.AddScoped<ITravelRepository, TravelRepository>();
        services.AddScoped<ITravelPointRepository, TravelPointRepository>();
        services.AddScoped<IPostcardRepository, PostcardRepository>();

        services.AddSingleton<ITravelPolicy, TravelPolicy>();
        services.AddSingleton<IPostcardPolicy, PostcardPolicy>();
        //services.AddHostedService<PostcardBackgroundJobService>();
        services.AddTransient<ITravelsModuleApi, TravelsModuleApi>();

        return services;
    }
}