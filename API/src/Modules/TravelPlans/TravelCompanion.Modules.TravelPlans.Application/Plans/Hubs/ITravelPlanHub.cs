namespace TravelCompanion.Modules.TravelPlans.Api.Hubs;

// METHODS TO BE USED BY CLIENT
public interface ITravelPlanHub
{
    Task ReceivePlanUpdate(object plan);
}
