using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class InvitationRepository : IInvitationRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<Invitation> _invitations;

    public InvitationRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _invitations = _dbContext.Invitations;
    }

    public async Task AddAsync(Invitation invitation)
    {
        await _invitations.AddAsync(invitation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Invitation> GetAsync(Guid invitationId)
    {
        return await _invitations.SingleOrDefaultAsync(x => x.Id == invitationId);
    }

    public async Task<bool> ExistsByIdAsync(Guid invitationId)
    {
        return await _invitations.AnyAsync(x => x.Id == invitationId);
    }

    public async Task<bool> ExistsForUserAndTravelPlanAsync(Guid userId, Guid travelPlanId)
    {
        return await _invitations.AnyAsync(
            x => x.ParticipantId == userId && x.TravelPlanId == travelPlanId);
    }

    public async Task UpdateAsync(Invitation invitation)
    {
        _invitations.Update(invitation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid invitationId)
    {
        var invitation = await GetAsync(invitationId);
        _invitations.Remove(invitation);
        await _dbContext.SaveChangesAsync();
    }
}