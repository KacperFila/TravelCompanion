namespace TravelCompanion.Modules.Travels.Core.DTO;

internal class TravelDetailsDTO
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
    public bool IsFinished { get; set; }
    public float? Rating { get; set; }
    public decimal AdditionalCosts { get; set; }
    public decimal TotalCosts { get; set; }
    public List<TravelPointDTO> TravelPoints { get; set; }
}