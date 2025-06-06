﻿using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public class Travel : IAuditable
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public IList<Guid>? ParticipantIds { get; set; }
    public List<TravelPoint> TravelPoints { get; set; }
    public List<Receipt> AdditionalCosts { get; set; }
    public Money AdditionalCostsValue { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
    public bool AllParticipantsPaid { get; set; }
    public bool IsFinished { get; set; }
    public List<TravelRating> Ratings { get; set; }
    public float? RatingValue { get; set; }
    public Money TotalCostsValue { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}