namespace TravelCompanion.Modules.Users.Shared.DTO;

public class UserInfoDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid? ActivePlanId { get; set; }
}