using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Modules.Travels.Shared;
using TravelCompanion.Modules.Travels.Shared.DTO;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal class TravelsModuleApi : ITravelsModuleApi
{
    private readonly IPostcardRepository _postcardRepository;

    public TravelsModuleApi(IPostcardRepository postcardRepository)
    {
        _postcardRepository = postcardRepository;
    }

    public async Task<List<PostcardDto>> GetUserLastYearPostcardsFromMonth(Guid userId, int month)
    {
        var postcards = await _postcardRepository.GetLastYearPostcardsFromMonth(userId, month);
        return postcards.Select(AsPostcardDto).ToList();
    }

    private static PostcardDto AsPostcardDto(Postcard postcard)
    {
        return new PostcardDto()
        {
            AddedById = postcard.AddedById,
            Description = postcard.Description,
            PhotoUrl = postcard.PhotoUrl,
            Title = postcard.Title
        };
    }
}