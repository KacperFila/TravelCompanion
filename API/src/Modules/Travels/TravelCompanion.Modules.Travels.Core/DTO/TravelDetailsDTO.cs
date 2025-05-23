namespace TravelCompanion.Modules.Travels.Core.DTO;

internal class TravelDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
    public bool IsFinished { get; set; }
    public float? Rating { get; set; }
    public decimal? AdditionalCostsValue { get; set; }
    public decimal? TotalCostsValue { get; set; }
    public List<TravelPointDto>? TravelPoints { get; set; }
}