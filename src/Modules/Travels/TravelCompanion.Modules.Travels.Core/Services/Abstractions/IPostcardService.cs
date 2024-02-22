using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Entities.Enums;

namespace TravelCompanion.Modules.Travels.Core.Services.Abstractions;

internal interface IPostcardService
{
    Task AddToTravelAsync(PostcardDto postcard, Guid travelId);
    Task<PostcardDto> GetAsync(Guid postcardId);
    Task<IReadOnlyList<PostcardDto>> GetAllByTravelIdAsync(Guid travelId);
    Task ChangeStatus(Guid postcardId, PostcardStatus postcardStatus);
    Task UpdateAsync(PostcardDto postcard, Guid postcardId);
    Task DeleteAsync(Guid postcardId);
}