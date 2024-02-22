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

    public async Task<bool> DoesUserOwnOrParticipateInPostcardTravel(Guid userId, Travel travel)
    {
        return travel.OwnerId == userId || (travel.ParticipantIds?.Contains(userId) ?? false); // participantsId jest nullem jeśli nie dodano nikogo (jedoosobowy travel)
    }

    //TODO refactor repeating code?
    public async Task<bool> DoesUserOwnPostcardTravel(Guid userId, Travel travel)
    {
        return travel.OwnerId == userId;
    }

    public async Task<bool> DoesUserParticipateInPostcardTravel(Guid userId, Travel travel)
    {
        return travel.ParticipantIds?.Contains(userId) ?? false;
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