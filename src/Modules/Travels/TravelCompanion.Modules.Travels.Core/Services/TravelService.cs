using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Modules.Travels.Core.Policies;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal class TravelService : ITravelService
{
    private readonly ITravelRepository _travelRepository;
    private readonly IDeleteTravelPolicy _deleteTravelPolicy;
    private readonly IContext _context;

    public TravelService(ITravelRepository travelRepository, IDeleteTravelPolicy deleteTravelPolicy, IContext context)
    {
        _travelRepository = travelRepository;
        _deleteTravelPolicy = deleteTravelPolicy;
        _context = context;
    }

    //TODO Remove after implementing TravelPlan -> Travel
    public async Task AddAsync(TravelDto travel)
    {
        var item = new Travel
        {
            Id = Guid.NewGuid(),
            OwnerId = _context.Identity.Id,
            Title = travel.Title,
            Description = travel.Description,
            From = travel.From,
            To = travel.To,
            ParticipantIds = null,
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

    public async Task UpdateAsync(Guid TravelId, TravelDto dto)
    {
        var travel = await _travelRepository.GetAsync(TravelId);

        if (travel.OwnerId != _context.Identity.Id)
        {
            throw new TravelDoesNotBelongToUserException(TravelId);
        }

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

    public async Task DeleteAsync(Guid TravelId)
    {
        var travel = await _travelRepository.GetAsync(TravelId);

        if (travel.OwnerId != _context.Identity.Id)
        {
            throw new TravelDoesNotBelongToUserException(TravelId);
        }

        if (travel is null)
        {
            throw new TravelNotFoundException(TravelId);
        }

        if (!await _deleteTravelPolicy.CanDeleteAsync(travel))
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