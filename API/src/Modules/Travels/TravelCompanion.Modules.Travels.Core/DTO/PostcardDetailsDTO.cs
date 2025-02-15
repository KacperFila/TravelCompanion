namespace TravelCompanion.Modules.Travels.Core.DTO;

internal class PostcardDetailsDTO
{
    public Guid PostcardId { get; set; }
    public string PostcardStatus { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public Guid AddedById { get; set; }
}