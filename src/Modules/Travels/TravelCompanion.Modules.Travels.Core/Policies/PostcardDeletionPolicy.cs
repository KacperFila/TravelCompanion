using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Entities.Enums;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.Travels.Core.Policies;
internal sealed class PostcardDeletionPolicy : IPostcardDeletionPolicy
{
    private readonly IContext _context;

    public PostcardDeletionPolicy(IContext context)
    {
        _context = context;
    }

    public async Task<bool> CanDeletePostcard(Postcard postcard, Travel travel)
    {
 
        var isUserTravelOwner = _context.Identity.Id == travel.OwnerId;

        if (isUserTravelOwner)
        {
            return true; // if users owns the travel - he can do anything
        }

        if (postcard.AddedById == _context.Identity.Id && postcard.Status == PostcardStatus.Pending)
        {
            return true; // if user created the postcard and it was not accepted yet
        }

        return false;
    }

    public async Task<bool> CanEditPostcard(Postcard postcard, Travel travel)
    {

        var isUserTravelOwner = _context.Identity.Id == travel.OwnerId;

        if (isUserTravelOwner)
        {
            return true; // if users owns the travel - he can do anything
        }

        if (postcard.AddedById == _context.Identity.Id && postcard.Status == PostcardStatus.Pending)
        {
            return true; // if user created the postcard and it was not accepted yet
        }

        return false;
    }
}