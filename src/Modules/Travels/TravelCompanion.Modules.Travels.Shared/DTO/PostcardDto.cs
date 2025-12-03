namespace TravelCompanion.Modules.Travels.Shared.DTO;

public record PostcardDto
{
    public Guid AddedById { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
}