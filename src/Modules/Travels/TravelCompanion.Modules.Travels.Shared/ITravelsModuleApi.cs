using TravelCompanion.Modules.Travels.Shared.DTO;

namespace TravelCompanion.Modules.Travels.Shared;

public interface ITravelsModuleApi
{
    Task<List<PostcardDto>> GetUserLastYearPostcardsFromMonth(Guid userId, int month);
}