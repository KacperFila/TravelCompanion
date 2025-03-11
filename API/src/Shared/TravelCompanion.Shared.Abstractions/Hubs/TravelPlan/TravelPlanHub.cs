using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Hubs.TravelPlan;

internal class TravelPlanHub : Hub<ITravelPlanHub>
{
    public async Task SendRoadmapUpdate(object plan)
    {
        await Clients.All.ReceivePlanUpdate(plan);
    }
}