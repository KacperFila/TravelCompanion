using System.ComponentModel.DataAnnotations;

namespace TravelCompanion.Modules.Users.Core.DTO
{
    public class SignInDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}