namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.DTO;

internal sealed class PlanInvitationResponse
{
    public Guid InvitationId { get; set; }
    public Guid PlanId { get; set; }
    public string InviterName { get; set; }
    public string PlanTitle { get; set; }
    public DateTime InvitationDate { get; set; }
}