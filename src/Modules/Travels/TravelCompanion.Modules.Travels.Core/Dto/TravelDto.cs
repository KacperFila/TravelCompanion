﻿namespace TravelCompanion.Modules.Travels.Core.Dto;

public class TravelDto
{ 
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
    public bool allParticipantsPaid { get; set; }
    public bool isFinished { get; set; }
    public float? Rating { get; set; }
}