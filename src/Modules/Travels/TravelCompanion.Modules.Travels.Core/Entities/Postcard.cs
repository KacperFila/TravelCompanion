using TravelCompanion.Modules.Travels.Core.Entities.Enums;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public class Postcard
{
    public Guid Id { get; set; }
    public Guid AddedById { get; set; }
    public Guid TravelId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public PostcardStatus Status { get; set; }

}