﻿using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
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
    private readonly ITravelPolicy _travelPolicy;
    private readonly IContext _context;
    private readonly Guid _userId;

    public TravelService(ITravelRepository travelRepository, ITravelPolicy travelDeletionPolicy, IContext context)
    {
        _travelRepository = travelRepository;
        _travelPolicy = travelDeletionPolicy;
        _context = context;
        _userId = _context.Identity.Id;
    }


    //TODO Remove after implementing TravelPlan -> Travel
    public async Task AddAsync(TravelUpsertDTO travelUpsert)
    {
        var item = new Travel
        {
            Id = Guid.NewGuid(),
            OwnerId = _userId,
            Title = travelUpsert.Title,
            Description = travelUpsert.Description,
            From = travelUpsert.From,
            To = travelUpsert.To,
            ParticipantIds = null
        };
 
        await _travelRepository.AddAsync(item);
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

    public async Task<IReadOnlyList<TravelDetailsDTO>> GetAllAsync()
    {
        var travels = await _travelRepository.GetAllAsync();

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

        if (!await _travelPolicy.CanDeleteAsync(travel))
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
            isFinished = travel.isFinished,
            Title = travel.Title,
            Rating = travel.Rating
        };
    }
}