using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}