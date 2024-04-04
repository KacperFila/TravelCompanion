using Microsoft.Extensions.DependencyInjection;

namespace TravelCompanion.Shared.Infrastructure.Emails;

public static class Extensions
{
    public static IServiceCollection AddEmails(this IServiceCollection services)
    {
        services.AddOptions<EmailOptions>().BindConfiguration("email");
        return services;
    }
}