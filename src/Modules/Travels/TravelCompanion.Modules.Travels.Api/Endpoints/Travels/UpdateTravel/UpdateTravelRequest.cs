using TravelCompanion.Modules.Travels.Core.Dto;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.UpdateTravel;

internal class UpdateTravelRequest
{
    public Guid TravelId { get; set; }
    public TravelDto Travel { get; set; }
}