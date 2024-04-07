using System.ComponentModel.DataAnnotations;

namespace TravelCompanion.Modules.Users.Core.DTO
{
    public class SignInDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}