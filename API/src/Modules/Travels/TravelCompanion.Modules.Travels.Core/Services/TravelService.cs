﻿using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Events;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
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

    public TravelService(ITravelRepository travelRepository, ITravelPolicy travelDeletionPolicy, IContext context, ITravelPointRepository travelPointRepository, IMessageBroker messageBroker)
    {
        _travelRepository = travelRepository;
        _travelPolicy = travelDeletionPolicy;
        _context = context;
        _travelPointRepository = travelPointRepository;
        _messageBroker = messageBroker;
        _userId = _context.Identity.Id;
    }


    public async Task<TravelDetailsDTO> GetAsync(Guid TravelId)
    {
        var travel = await _travelRepository.GetAsync(TravelId);

        if (travel is null)
        {
            return null;
        }

        return AsTravelDetailsDto(travel);
    }

    public async Task<IReadOnlyList<TravelDetailsDTO>> GetAllAsync(string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var travels = await _travelRepository.GetAllAsync(searchTerm, sortColumn, sortOrder);

        var dtos = travels.Select(AsTravelDetailsDto).ToList();

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

        if (!_travelPolicy.DoesUserParticipate(travel, _userId))
        {
            throw new UserDoesNotParticipateInTravelException(TravelId);
        }

        var currentRating = travel.Ratings.FirstOrDefault(x => x.AddedBy == _userId);

        if (currentRating is not null)
        {
            currentRating.Value = Rating;
            await _travelRepository.UpdateTravelRatingAsync(currentRating);
        }
        else
        {
            var travelRating = new TravelRating()
            {
                Id = Guid.NewGuid(),
                AddedBy = _userId,
                TravelId = TravelId,
                Value = Rating
            };
            await _travelRepository.AddTravelRatingAsync(travelRating);
        }

        var ratingValue = travel.Ratings.Average(x => x.Value);
        travel.RatingValue = ratingValue;
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

        var travelRating = travel.Ratings.SingleOrDefault(x => x.AddedBy == _userId);

        if (travelRating is not null)
        {
            await _travelRepository.RemoveTravelRatingAsync(travelRating);
        }

        travel.RatingValue = !travel.Ratings.Any() ? null : travel.Ratings.Average(x => x.Value);

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

        if (!_travelPolicy.CanDelete(travel))
        {
            throw new TravelCannotBeDeletedException(TravelId);
        }
    }

    private static TravelDetailsDTO AsTravelDetailsDto(Travel travel)
    {
        return new TravelDetailsDTO()
        {
            Description = travel.Description,
            From = travel.From,
            To = travel.To,
            IsFinished = travel.IsFinished,
            Title = travel.Title,
            Rating = travel.RatingValue,
            AdditionalCosts = travel.AdditionalCostsValue.Amount,
            TotalCosts = travel.TotalCostsValue.Amount,
            TravelPoints = travel.TravelPoints.Select(AsTravelPointDto).ToList(),
        };
    }

    private static TravelPointDTO AsTravelPointDto(TravelPoint point)
    {
        return new TravelPointDTO()
        {
            Id = point.TravelPointId,
            PlaceName = point.PlaceName,
            TotalCost = point.TotalCost.Amount
        };
    }
}