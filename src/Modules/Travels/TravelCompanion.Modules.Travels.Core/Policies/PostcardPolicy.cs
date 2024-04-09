using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Entities.Enums;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.Travels.Core.Policies;

internal sealed class PostcardPolicy : IPostcardPolicy
{
    private readonly IContext _context;

    public PostcardPolicy(IContext context)
    {
        _context = context;
    }

    public bool DoesUserOwnPostcardTravel(Guid userId, Travel travel)
    {
        return travel.OwnerId == userId;
    }

    public bool DoesUserParticipateInPostcardTravel(Guid userId, Travel travel)
    {
        return travel.ParticipantIds?.Contains(userId) ?? false;
    }

    public bool CanDeletePostcard(Postcard postcard, Travel travel)
    {

        var isUserTravelOwner = _context.Identity.Id == travel.OwnerId;

        if (isUserTravelOwner)
        {
            return true;
        }

        if (postcard.AddedById == _context.Identity.Id && postcard.Status == PostcardStatus.Pending) //TODO change from enums
        {
            return true;
        }

        return false;
    }

    public bool CanEditPostcard(Postcard postcard, Travel travel)
    {

        var isUserTravelOwner = _context.Identity.Id == travel.OwnerId;

        if (isUserTravelOwner)
        {
            return true;
        }

        if (postcard.AddedById == _context.Identity.Id && postcard.Status == PostcardStatus.Pending)
        {
            return true;
        }

        return false;
    }
}