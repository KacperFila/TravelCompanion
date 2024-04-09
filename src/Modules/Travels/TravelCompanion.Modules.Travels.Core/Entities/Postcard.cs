using TravelCompanion.Modules.Travels.Core.Entities.Enums;
using TravelCompanion.Shared.Abstractions.Kernel;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public class Postcard : IAuditable
{
    public Guid Id { get; set; }
    public Guid AddedById { get; set; }
    public Guid TravelId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public string Status { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}