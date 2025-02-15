
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Abstractions.Messaging;
using TravelCompanion.Shared.Infrastructure.Messaging.Brokers;
using TravelCompanion.Shared.Infrastructure.Messaging.Dispatchers;

namespace TravelCompanion.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        private const string SectionName = "messaging";
        
        internal static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, MessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();

            var messagingOptions = services.GetOptions<MessagingOptions>(SectionName);
            services.AddSingleton(messagingOptions);

            if (messagingOptions.UseBackgroundDispatcher)
            {
                services.AddHostedService<BackgroundDispatcher>();
            }
            
            return services;
        }
    }
}