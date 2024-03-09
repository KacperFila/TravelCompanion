using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.DTO;

internal class TravelUpsertDTO
{ 
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public bool AllParticipantsPaid { get; set; }
    public List<Receipt> AdditionalCosts { get; set; }
    public Money AdditionalCostsValue { get; set; }
    public bool IsFinished { get; set; }
    public float? Rating { get; set; }
}