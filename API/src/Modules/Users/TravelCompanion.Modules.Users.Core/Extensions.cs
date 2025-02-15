using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.Users.Core.DAL.Repositories;
using TravelCompanion.Modules.Users.Core.DAL;
using TravelCompanion.Modules.Users.Core.Entities;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Modules.Users.Core.Services;
using System.Runtime.CompilerServices;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("TravelCompanion.Modules.Users.Api")]

namespace TravelCompanion.Modules.Users.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IUsersModuleApi, UsersModuleApi>()
                .AddPostgres<UsersDbContext>();
    }
}