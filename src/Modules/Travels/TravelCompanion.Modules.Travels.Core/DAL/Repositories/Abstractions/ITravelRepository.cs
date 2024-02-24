﻿using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;

public interface ITravelRepository
{
    Task<Travel> GetAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<List<Travel>> GetAllAsync();
    Task AddAsync(Travel travel);
    Task UpdateAsync(Travel travel);
    Task DeleteAsync(Guid id);
    Task AddTravelRatingAsync(TravelRating travelRating);
    Task UpdateTravelRatingAsync(TravelRating travelRating);

    Task RemoveTravelRatingAsync(TravelRating travelRating);
}