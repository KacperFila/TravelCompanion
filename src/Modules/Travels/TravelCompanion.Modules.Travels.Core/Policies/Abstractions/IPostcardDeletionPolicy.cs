using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

internal interface IPostcardDeletionPolicy
{
    Task<bool> CanDeletePostcard(Postcard postcard, Travel travel);
}