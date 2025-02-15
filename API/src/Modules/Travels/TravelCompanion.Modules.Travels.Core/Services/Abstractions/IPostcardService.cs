using TravelCompanion.Modules.Travels.Core.DTO;

namespace TravelCompanion.Modules.Travels.Core.Services.Abstractions;

internal interface IPostcardService
{
    Task AddToTravelAsync(PostcardUpsertDTO postcard, Guid travelId);
    Task<PostcardDetailsDTO> GetAsync(Guid postcardId);
    Task<IReadOnlyList<PostcardDetailsDTO>> GetAllByTravelIdAsync(Guid travelId);
    Task ChangeStatus(Guid postcardId, string postcardStatus);
    Task UpdateAsync(PostcardUpsertDTO postcard, Guid postcardId);
    Task DeleteAsync(Guid postcardId);
}