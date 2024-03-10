using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories;

internal class TravelPointRepository : ITravelPointRepository
{
    private readonly TravelsDbContext _context;
    private readonly DbSet<TravelPoint> _travelPoints;
    public TravelPointRepository(TravelsDbContext context)
    {
        _context = context;
        _travelPoints = _context.TravelPoints;
    }

    public async Task<List<TravelPoint>> GetForTravelAsync(Guid travelId)
    {
        return await _travelPoints.Where(x => x.TravelId == travelId).ToListAsync();
    }
}