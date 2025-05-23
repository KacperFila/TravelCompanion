using TravelCompanion.Modules.Travels.Core.DTO;

namespace TravelCompanion.Modules.Travels.Core.Services.Abstractions;

internal interface IPostcardService
{
    Task AddToTravelAsync(PostcardUpsertDto postcard, Guid travelId);
    Task<PostcardDetailsDto> GetAsync(Guid postcardId);
    Task<IReadOnlyList<PostcardDetailsDto>> GetAllByTravelIdAsync(Guid travelId);
    Task ChangeStatus(Guid postcardId, string postcardStatus);
    Task UpdateAsync(PostcardUpsertDto postcard, Guid postcardId);
    Task DeleteAsync(Guid postcardId);
}