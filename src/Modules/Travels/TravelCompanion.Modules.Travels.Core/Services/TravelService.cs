using FluentValidation;
using Humanizer;
using Microsoft.Extensions.Hosting;
using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Modules.Travels.Core.Policies;
using TravelCompanion.Modules.Travels.Core.Repositories;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal class TravelService : ITravelService
{
    private readonly ITravelRepository _travelRepository;
    private readonly AbstractValidator<Travel> _validator;
    private readonly IDeleteTravelPolicy _deleteTravelPolicy;

    public TravelService(ITravelRepository travelRepository, AbstractValidator<Travel> validator, IDeleteTravelPolicy deleteTravelPolicy)
    {
        _travelRepository = travelRepository;
        _validator = validator;
        _deleteTravelPolicy = deleteTravelPolicy;
    }

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
        return travel is null ? null : AsTravelDto(travel);
    }

    public async Task<IReadOnlyList<TravelDto>> GetAllAsync()
    {
        var travels = await _travelRepository.GetAllAsync();

        var dtos = travels.Select(AsTravelDto).ToList();

        return dtos;
    }

    public async Task UpdateAsync(TravelDto dto)
    {
        //TODO Add check for owner
        var travel = await _travelRepository.GetAsync(dto.Id);

        if (travel is null)
        {
            throw new TravelNotFoundException(dto.Id);
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
            Id = travel.Id,
            isFinished = travel.isFinished,
            OwnerId = travel.OwnerId,
            Title = travel.Title,
        };
    }
}