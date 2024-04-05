using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Shared.Infrastructure.Emails;

public static class Extensions
{
    public static IServiceCollection AddEmails(this IServiceCollection services)
    {
        services.AddOptions<EmailOptions>().BindConfiguration("email");
        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }
}