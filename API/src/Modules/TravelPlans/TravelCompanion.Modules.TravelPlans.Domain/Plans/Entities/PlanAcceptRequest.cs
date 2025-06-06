﻿using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class PlanAcceptRequest : IAuditable
{
    public Guid Id { get; private set; }
    public AggregateId PlanId { get; private set; }
    public IList<EntityId>? ParticipantsAccepted { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public PlanAcceptRequest(AggregateId planId)
    {
        Id = Guid.NewGuid();
        PlanId = planId;
        ParticipantsAccepted = new List<EntityId>();
    }

    public static PlanAcceptRequest Create(AggregateId planId)
    {
        return new PlanAcceptRequest(planId);
    }

    public void AddParticipantAcceptation(Guid participantId)
    {
        if (participantId == Guid.Empty)
        {
            throw new InvalidParticipantException(participantId);
        }

        if (ParticipantsAccepted.Contains(participantId))
        {
            throw new ParticipantAlreadyAcceptedException(participantId);
        }

        ParticipantsAccepted.Add(participantId);
    }

    public void RemoveParticipantAcceptation(Guid participantId)
    {
        if (participantId == Guid.Empty)
        {
            throw new InvalidParticipantException(participantId);
        }

        if (!ParticipantsAccepted.Contains(participantId))
        {
            throw new UserCannotDenyAcceptRequestException(participantId);
        }

        ParticipantsAccepted.Remove(participantId);
    }
}