using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class TravelPlanInvitationRepository : ITravelPlanInvitationRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<TravelPlanInvitation> _travelPlanInvitations;

    public TravelPlanInvitationRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _travelPlanInvitations = _dbContext.TravelPlanInvitations;
    }

    public async Task AddInvitationAsync(TravelPlanInvitation travelPlanInvitation)
    {
        await _travelPlanInvitations.AddAsync(travelPlanInvitation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TravelPlanInvitation> GetAsync(Guid invitationId)
    {
        return await _travelPlanInvitations.SingleOrDefaultAsync(x => x.Id == invitationId);
    }

    public async Task<bool> ExistsByIdAsync(Guid invitationId)
    {
        return await _travelPlanInvitations.AnyAsync(x => x.Id == invitationId);
    }

    public async Task<bool> ExistsForUserAndTravelPlanAsync(Guid userId, Guid travelPlanId)
    {
        return await _travelPlanInvitations.AnyAsync(
            x => x.ParticipantId == userId && x.TravelPlanId == travelPlanId);
    }

    public async Task UpdateInvitationAsync(TravelPlanInvitation travelPlanInvitation)
    {
        _travelPlanInvitations.Update(travelPlanInvitation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveInvitationAsync(Guid invitationId)
    {
        var invitation = await GetAsync(invitationId);
        _travelPlanInvitations.Remove(invitation);
        await _dbContext.SaveChangesAsync();
    }
}