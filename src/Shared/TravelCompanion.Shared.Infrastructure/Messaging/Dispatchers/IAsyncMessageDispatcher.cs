using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Shared.Infrastructure.Messaging.Dispatchers
{
    internal interface IAsyncMessageDispatcher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}