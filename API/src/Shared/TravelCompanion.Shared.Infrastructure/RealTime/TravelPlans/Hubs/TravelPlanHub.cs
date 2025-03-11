using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans.Hubs;

internal class TravelPlanHub : Hub<ITravelPlanHub>
{
    public async Task SendRoadmapUpdate(object plan)
    {
        await Clients.All.ReceivePlanUpdate(plan);
    }
}