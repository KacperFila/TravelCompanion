﻿using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Entities.Enums;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal sealed class PostcardService : IPostcardService
{
    private readonly IPostcardRepository _postcardRepository;
    private readonly ITravelRepository _travelRepository;
    private readonly IPostcardPolicy _postcardPolicy;
    private readonly IContext _context;
    private readonly Guid _userId;

    public PostcardService(IPostcardRepository postcardRepository, IContext context, ITravelRepository travelRepository, IPostcardPolicy postcardPolicy)
    {
        _postcardRepository = postcardRepository;
        _context = context;
        _travelRepository = travelRepository;
        _postcardPolicy = postcardPolicy;
        _userId = _context.Identity.Id;
    }

    public async Task AddToTravelAsync(PostcardUpsertDTO postcard, Guid travelId)
    {
        var travel = await _travelRepository.GetAsync(travelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(travelId);
        }

        if (!_postcardPolicy.DoesUserOwnOrParticipateInPostcardTravel(_userId, travel))
        {
            throw new UserDoesNotParticipateInTravelException(_userId);
        }

        var isCurrentUserTravelOwner = _postcardPolicy.DoesUserOwnPostcardTravel(_userId, travel);
        
        var postcardsStatus = isCurrentUserTravelOwner ? PostcardStatus.Accepted : PostcardStatus.Pending;

        var item = new Postcard()
        {
            Id = Guid.NewGuid(),
            TravelId = travelId,
            Title = postcard.Title,
            Description = postcard.Description,
            PhotoUrl = postcard.PhotoUrl,
            Status = postcardsStatus,
            AddedById = _userId
        };

        await _postcardRepository.AddAsync(item);
    }

    public async Task<PostcardDetailsDTO> GetAsync(Guid postcardId)
    {
        var postcard = await _postcardRepository.GetAsync(postcardId);

        if (postcard is null)
        {
            return null;
        }

        return AsPostcardDetailsDto(postcard);
    }

    public async Task<IReadOnlyList<PostcardDetailsDTO>> GetAllByTravelIdAsync(Guid travelId)
    {
        var travel = await _travelRepository.GetAsync(travelId);
        if (travel is null)
        {
            throw new TravelNotFoundException(travelId);
        }

        if (!_postcardPolicy.DoesUserOwnOrParticipateInPostcardTravel(_userId, travel))
        {
            throw new UserDoesNotParticipateInTravelException(travelId);
        }

        var postcards = await _postcardRepository.GetAllByTravelIdAsync(travelId);

        if (!postcards.Any())
        {
            throw new NoPostcardsForTravelFoundException(travelId);
        }

        var dtos = postcards.Select(AsPostcardDetailsDto).ToList();

        return dtos;
    }

    public async Task ChangeStatus(Guid postcardId, PostcardStatus postcardStatus)
    {
        var postcard = await _postcardRepository.GetAsync(postcardId);
        
        if (postcard is null)
        {
            throw new PostcardNotFoundException(postcardId);
        }

        var travel = await _travelRepository.GetAsync(postcard.TravelId);
        
        if (!_postcardPolicy.DoesUserOwnPostcardTravel(_userId, travel))
        {
            throw new UserCannotManagePostcardException(postcard.TravelId);
        }

        postcard.Status = postcardStatus;
        await _postcardRepository.UpdateAsync(postcard);
    }

    public async Task UpdateAsync(PostcardUpsertDTO postcard, Guid postcardId)
    {
        var item = await _postcardRepository.GetAsync(postcardId);
        
        if (item is null)
        {
            throw new PostcardNotFoundException(postcardId);
        }

        var travel = await _travelRepository.GetAsync(item.TravelId);
        
        if (!_postcardPolicy.DoesUserOwnOrParticipateInPostcardTravel(_userId, travel))
        {
            throw new UserCannotManagePostcardException(item.Id);
        }

        if(!_postcardPolicy.CanDeletePostcard(item, travel))
        {
            throw new UserCannotManagePostcardException(item.Id);
        }

        item.Description = postcard.Description;
        item.PhotoUrl = postcard.PhotoUrl;
        item.Title = postcard.Title;

        await _postcardRepository.UpdateAsync(item);
    }

    public async Task DeleteAsync(Guid postcardId)
    {
        var postcard = await _postcardRepository.GetAsync(postcardId);

        if (postcard is null)
        {
            throw new PostcardNotFoundException(postcardId);
        }

        var travel = await _travelRepository.GetAsync(postcard.TravelId);

        if (!_postcardPolicy.CanDeletePostcard(postcard, travel))
        {
            throw new PostcardCannotBeDeletedException(postcardId);
        }

        await _postcardRepository.DeleteAsync(postcardId);
    }

    private static PostcardDetailsDTO AsPostcardDetailsDto(Postcard postcard)
    {
        return new PostcardDetailsDTO()
        {
            AddedById = postcard.AddedById,
            PostcardId = postcard.Id,
            PostcardStatus = postcard.Status.ToString(),
            Description = postcard.Description,
            PhotoUrl = postcard.PhotoUrl,
            Title = postcard.Title
        };
    }
}