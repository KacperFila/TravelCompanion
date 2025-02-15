using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

internal interface ITravelPolicy
{
    bool CanDelete(Travel travel);
    bool IsUserOwnerOrParticipant(Travel travel, Guid userId);
    bool DoesUserParticipate(Travel travel, Guid userId);
}