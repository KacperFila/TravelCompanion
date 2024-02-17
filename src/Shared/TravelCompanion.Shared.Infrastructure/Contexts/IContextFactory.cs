using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Shared.Infrastructure.Contexts
{
    internal interface IContextFactory
    {
        IContext Create();
    }
}