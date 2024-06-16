using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs
{
    public class RegistroDTO
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Type User is required")]
        public string? TipoUsuario { get; set; }
    }
}
