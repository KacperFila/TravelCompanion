using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Events;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Messaging;
using TravelPointNotFoundException = TravelCompanion.Modules.Travels.Core.Exceptions.TravelPointNotFoundException;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal class TravelService : ITravelService
{
    private readonly ITravelRepository _travelRepository;
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly ITravelPolicy _travelPolicy;
    private readonly IContext _context;
    private readonly Guid _userId;
    private readonly IMessageBroker _messageBroker;
    private readonly IUsersModuleApi _usersModuleApi;

    public TravelService(ITravelRepository travelRepository, ITravelPolicy travelDeletionPolicy, IContext context, ITravelPointRepository travelPointRepository, IMessageBroker messageBroker, IUsersModuleApi usersModuleApi)
    {
        _travelRepository = travelRepository;
        _travelPolicy = travelDeletionPolicy;
        _context = context;
        _travelPointRepository = travelPointRepository;
        _messageBroker = messageBroker;
        _usersModuleApi = usersModuleApi;
        _userId = _context.Identity.Id;
    }


    public async Task<TravelDetailsDto?> GetAsync(Guid travelId)
    {
        var travel = await _travelRepository.GetAsync(travelId);

        return travel is not null 
            ? AsTravelDetailsDto(travel)
            : null;
    }

    public async Task<TravelDetailsDto?> GetActiveAsync()
    {
        var userInfo = await _usersModuleApi.GetUserInfo(_userId);

        if (userInfo.ActiveTravelId is null)
        {
            throw new NoActiveTravelForUserException(_userId);
        }
        
        var travel = await _travelRepository.GetAsync((Guid)userInfo.ActiveTravelId);

        return travel is not null 
            ? AsTravelDetailsDto(travel)
            : null;
    }

    public async Task<IReadOnlyList<TravelDetailsDto?>> GetAllAsync(string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var travels = await _travelRepository.GetAllAsync(searchTerm, sortColumn, sortOrder);

        var travelsDtos = travels
            .Select(AsTravelDetailsDto)
            .ToList();

        return travelsDtos;
    }

    public async Task ChangeActiveTravelAsync(Guid travelId)
    {
        var travelExists = await _travelRepository.ExistAsync(travelId);

        if (!travelExists)
        {
            throw new TravelNotFoundException(travelId);
        }

        var travel = await _travelRepository.GetAsync(travelId);

        if (travel!.ParticipantIds!.All(x => x != _userId))
        {
            throw new UserDoesNotParticipateInTravelException(travelId);
        }

        await _messageBroker.PublishAsync(new ActiveTravelChanged(_userId, travelId));
    }

    public async Task RateAsync(Guid travelId, int ratingValue)
    {
        if (ratingValue is < 1 or > 5)
        {
            throw new RatingOutOfRangeException();
        }

        var travel = await _travelRepository.GetAsync(travelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(travelId);
        }

        if (!_travelPolicy.DoesUserParticipate(travel, _userId))
        {
            throw new UserDoesNotParticipateInTravelException(travelId);
        }

        var currentRating = travel.Ratings
            .FirstOrDefault(x => x.AddedBy == _userId);

        if (currentRating is not null)
        {
            currentRating.Value = ratingValue;
            await _travelRepository.UpdateTravelRatingAsync(currentRating);
        }
        else
        {
            var travelRating = new TravelRating()
            {
                Id = Guid.NewGuid(),
                AddedBy = _userId,
                TravelId = travelId,
                Value = ratingValue
            };
            await _travelRepository.AddTravelRatingAsync(travelRating);
        }

        var averageRating = travel.Ratings.Average(x => x.Value);
        travel.RatingValue = averageRating;
        await _travelRepository.UpdateAsync(travel);
    }

    public async Task VisitTravelPointAsync(Guid pointId)
    {
        var point = await _travelPointRepository.GetAsync(pointId);

        if (point is null)
        {
            throw new TravelPointNotFoundException(pointId);
        }

        if (point.IsVisited)
        {
            throw new TravelPointAlreadyVisitedException(pointId);
        }

        var travel = await _travelRepository.GetAsync(point.TravelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(point.TravelId);
        }
        
        if (travel.OwnerId != _userId)
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        point.VisitTravelPoint();

        await _travelPointRepository.UpdateAsync(point);

        if (travel.TravelPoints.All(x => x.IsVisited))
        {
            await _messageBroker.PublishAsync(new TravelIsFinished(travel.Id));
        }
    }

    public async Task RemoveRatingAsync(Guid travelId)
    {
        var travel = await _travelRepository.GetAsync(travelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(travelId);
        }

        if (travel.OwnerId != _userId)
        {
            throw new TravelDoesNotBelongToUserException(travelId);
        }

        var travelRating = travel.Ratings
            .SingleOrDefault(x => x.AddedBy == _userId);

        if (travelRating is not null)
        {
            await _travelRepository.RemoveTravelRatingAsync(travelRating);
        }

        travel.RatingValue = travel.Ratings.Any() 
            ? travel.Ratings.Average(x => x.Value) 
            : null;

        await _travelRepository.UpdateAsync(travel);
    }

    public async Task DeleteAsync(Guid travelId)
    {
        var travel = await _travelRepository.GetAsync(travelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(travelId);
        }
        
        if (travel.OwnerId != _userId)
        {
            throw new TravelDoesNotBelongToUserException(travelId);
        }

        if (!_travelPolicy.CanDelete(travel))
        {
            throw new TravelCannotBeDeletedException(travelId);
        }
    }

    private static TravelDetailsDto AsTravelDetailsDto(Travel travel)
    {
        return new TravelDetailsDto
        {
            Id = travel.Id,
            Description = travel.Description,
            From = travel.From,
            To = travel.To,
            IsFinished = travel.IsFinished,
            Title = travel.Title,
            Rating = travel.RatingValue,
            AdditionalCostsValue = travel.AdditionalCostsValue.Amount,
            TotalCostsValue = travel.TotalCostsValue.Amount,
            TravelPoints = travel.TravelPoints.Select(AsTravelPointDto).ToList(),
        };
    }

    private static TravelPointDto AsTravelPointDto(TravelPoint point)
    {
        return new TravelPointDto
        {
            Id = point.TravelPointId,
            PlaceName = point.PlaceName,
            TotalCost = point.TotalCost.Amount
        };
    }
}