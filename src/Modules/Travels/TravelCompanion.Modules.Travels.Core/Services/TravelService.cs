using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal class TravelService : ITravelService
{
    private readonly ITravelRepository _travelRepository;
    private readonly ITravelDeletionPolicy _travelDeletionPolicy;
    private readonly IContext _context;
    private readonly Guid _userId;

    public TravelService(ITravelRepository travelRepository, ITravelDeletionPolicy travelDeletionPolicy, IContext context)
    {
        _travelRepository = travelRepository;
        _travelDeletionPolicy = travelDeletionPolicy;
        _context = context;
        _userId = _context.Identity.Id;
    }


    //TODO Remove after implementing TravelPlan -> Travel
    public async Task AddAsync(TravelDto travel)
    {
        var tempParticipantIds = new List<Guid>();
        tempParticipantIds.Add(Guid.Parse("10b0c715-d3c5-498c-be00-ec229231e8b5"));

        var item = new Travel
        {
            Id = Guid.NewGuid(),
            OwnerId = _userId,
            Title = travel.Title,
            Description = travel.Description,
            From = travel.From,
            To = travel.To,
            ParticipantIds = tempParticipantIds
        };
 
        await _travelRepository.AddAsync(item);
    }

    public async Task<TravelDto> GetAsync(Guid TravelId)
    {
        var travel = await _travelRepository.GetAsync(TravelId);

        if (travel is null)
        {
            return null;
        }

        return AsTravelDto(travel);
    }

    public async Task<IReadOnlyList<TravelDto>> GetAllAsync()
    {
        var travels = await _travelRepository.GetAllAsync();

        var dtos = travels.Select(AsTravelDto).ToList();

        return dtos;
    }

    public async Task RateAsync(Guid TravelId, int Rating)
    {
        if (Rating is < 1 or > 5)
        {
            throw new RatingOutOfRangeException();
        }

        var travel = await _travelRepository.GetAsync(TravelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(TravelId);
        }

        if (travel.OwnerId != _userId)
        {
            throw new TravelDoesNotBelongToUserException(TravelId);
        }

        travel.Rating = Rating;
        await _travelRepository.UpdateAsync(travel);
    }

    public async Task RemoveRatingAsync(Guid TravelId)
    {
        var travel = await _travelRepository.GetAsync(TravelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(TravelId);
        }

        if (travel.OwnerId != _userId)
        {
            throw new TravelDoesNotBelongToUserException(TravelId);
        }

        travel.Rating = null;
        await _travelRepository.UpdateAsync(travel);
    }

    public async Task DeleteAsync(Guid TravelId)
    {
        var travel = await _travelRepository.GetAsync(TravelId);

        if (travel.OwnerId != _userId)
        {
            throw new TravelDoesNotBelongToUserException(TravelId);
        }

        if (travel is null)
        {
            throw new TravelNotFoundException(TravelId);
        }

        if (!await _travelDeletionPolicy.CanDeleteAsync(travel))
        {
            throw new TravelCannotBeDeletedException(TravelId);
        }
    }

    private static TravelDto AsTravelDto(Travel travel)
    {
        return new TravelDto()
        {
            allParticipantsPaid = travel.allParticipantsPaid,
            Description = travel.Description,
            From = travel.From,
            To = travel.To,
            isFinished = travel.isFinished,
            OwnerId = travel.OwnerId,
            Title = travel.Title,
            Rating = travel.Rating
        };
    }
}