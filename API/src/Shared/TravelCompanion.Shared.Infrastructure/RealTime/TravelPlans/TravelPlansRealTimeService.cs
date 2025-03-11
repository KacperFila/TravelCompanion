using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;
using TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans.Hubs;

namespace TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans;

internal class TravelPlansRealTimeService : ITravelPlansRealTimeService
{
    private readonly IHubContext<TravelPlanHub, ITravelPlanHub> _hubContext;

    public TravelPlansRealTimeService(IHubContext<TravelPlanHub, ITravelPlanHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendRoadmapUpdate(object plan)
    {
        await _hubContext.Clients.All.ReceivePlanUpdate(plan);
    }
}

