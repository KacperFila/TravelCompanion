using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using TravelCompanion.Modules.Emails.Core.Services;
using TravelCompanion.Shared.Abstractions.Auth;
using TravelCompanion.Shared.Infrastructure.Auth;

[assembly: InternalsVisibleTo("TravelCompanion.Modules.Emails.Api")]
namespace TravelCompanion.Modules.Emails.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}