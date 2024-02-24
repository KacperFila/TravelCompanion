using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.DomainEvents;

public record ParticipantPaid(ParticipantId participantId) : IDomainEvent;