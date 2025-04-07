namespace TravelCompanion.Modules.Users.Shared.DTO;

public class UserInfoDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public Guid? ActivePlanId { get; set; }
}