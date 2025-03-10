using Microsoft.AspNetCore.SignalR;

namespace TravelCompanion.Modules.TravelPlans.Api.Hubs;

public  class TravelPlanHub : Hub<ITravelPlanHub>
{
    public async Task SendRoadmapUpdate(object plan)
    {
        await Clients.All.ReceivePlanUpdate(plan);
    }

}
