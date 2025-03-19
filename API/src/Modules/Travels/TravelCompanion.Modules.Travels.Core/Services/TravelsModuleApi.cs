using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Modules.Travels.Shared;
using TravelCompanion.Modules.Travels.Shared.DTO;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal class TravelsModuleApi : ITravelsModuleApi
{
    private readonly IPostcardRepository _postcardRepository;
    private readonly ITravelRepository _travelRepository;

    public TravelsModuleApi(IPostcardRepository postcardRepository, ITravelRepository travelRepository)
    {
        _postcardRepository = postcardRepository;
        _travelRepository = travelRepository;
    }

    public async Task<List<PostcardDto>> GetUserLastYearPostcardsFromMonth(Guid userId, int month)
    {
        var postcards = await _postcardRepository.GetLastYearPostcardsFromMonth(userId, month);
        return postcards.Select(AsPostcardDto).ToList();
    }

    public async Task<TravelDto> GetTravelInfo(Guid travelId)
    {
        var travel = await _travelRepository.GetAsync(travelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(travelId);
        }

        return new TravelDto()
        {
            TravelId = travel.Id,
            From = travel.From,
            To = travel.To,
            PointsAdditionalCostValue = travel.TravelPoints.Sum(x => x.TotalCost.Amount),
            TravelAdditionalCostValue = travel.AdditionalCosts.Sum(x => x.Amount.Amount),
            TotalCostValue = travel.TotalCostsValue.Amount,
            ParticipantsCosts = travel.AdditionalCosts.Select(AsReceiptDto).ToList()
        };
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

    private static ReceiptDto AsReceiptDto(Receipt receipt)
    {
        return new ReceiptDto()
        {
            Id = receipt.Id,
            Amount = receipt.Amount.Amount,
            TravelPointId = receipt.TravelPointId
        };
    }
}