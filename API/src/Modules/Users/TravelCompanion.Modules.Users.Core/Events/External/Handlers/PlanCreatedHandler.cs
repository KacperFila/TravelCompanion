using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Users.Core.Events.External.Handlers;

internal class PlanCreatedHandler : IEventHandler<PlanCreated>
{
    public Task HandleAsync(PlanCreated @event)
    {

        // TODO Implement plan created
        throw new System.NotImplementedException();
    }
}
