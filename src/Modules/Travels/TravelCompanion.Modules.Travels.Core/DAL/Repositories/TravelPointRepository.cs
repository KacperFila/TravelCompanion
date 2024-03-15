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
        return await _travelPoints
            .Where(x => x.TravelId == travelId)
            .ToListAsync();
    }

    public async Task<TravelPoint> GetAsync(Guid pointId)
    {
        return await _travelPoints
            .SingleOrDefaultAsync(x => x.TravelPointId == pointId);
    }

    public async Task UpdateAsync(TravelPoint travelPoint)
    {
        _travelPoints.Update(travelPoint);
        await _context.SaveChangesAsync();
    }
}