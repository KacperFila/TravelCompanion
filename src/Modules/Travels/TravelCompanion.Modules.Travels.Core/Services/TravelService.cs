using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Modules.Travels.Core.Policies;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal class TravelService : ITravelService
{
    private readonly ITravelRepository _travelRepository;
    private readonly IDeleteTravelPolicy _deleteTravelPolicy;

    public TravelService(ITravelRepository travelRepository, IDeleteTravelPolicy deleteTravelPolicy)
    {
        _travelRepository = travelRepository;
        _deleteTravelPolicy = deleteTravelPolicy;
    }

    //Do usuniecia potem
    public async Task AddAsync(TravelDto travel)
    {
        var item = new Travel
        {
            Id = Guid.NewGuid(),
            Description = travel.Description,
            From = travel.From,
            To = travel.To,
            Title = travel.Title,
            OwnerId = travel.OwnerId,
            ParticipantIds = null,
        };
 
        await _travelRepository.AddAsync(item);
    }

    public async Task<TravelDto> GetAsync(Guid id)
    {
        var travel = await _travelRepository.GetAsync(id);

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

    public async Task UpdateAsync(Guid TravelId, TravelDto dto)
    {
        //TODO Add check for owner
        var travel = await _travelRepository.GetAsync(TravelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(TravelId);
        }

        travel.Title = dto.Title;
        travel.Description = dto.Description;
        travel.From = dto.From;
        travel.To = dto.To;
        travel.allParticipantsPaid = dto.allParticipantsPaid;
        travel.isFinished = dto.isFinished;

        await _travelRepository.UpdateAsync(travel);
    }

    public async Task DeleteAsync(Guid id)
    {
        //TODO Add check for owner in the policy

        var travel = await _travelRepository.GetAsync(id);

        if (travel is null)
        {
            throw new TravelNotFoundException(id);
        }

        if (!await _deleteTravelPolicy.CanDeleteAsync(travel))
        {
            throw new TravelCannotBeDeletedException(id);
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