using System.ComponentModel.DataAnnotations;

namespace Tea_Store.DTOs.UsersDTO
{
    public class LoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(6)]
        public string Password { get; set; } = null!;
    }
}
