using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Kernel
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(params IDomainEvent[] events);
    }
}