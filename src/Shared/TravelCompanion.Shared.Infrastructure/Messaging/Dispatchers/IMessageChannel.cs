using System.Threading.Channels;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Shared.Infrastructure.Messaging.Dispatchers
{
    public interface IMessageChannel
    {
        ChannelReader<IMessage> Reader { get; }
        ChannelWriter<IMessage> Writer { get; }
    }
}