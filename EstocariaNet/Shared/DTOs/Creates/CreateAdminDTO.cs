using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs.Creates
{
    public class CreateAdminDTO
    {
        [Required(ErrorMessage = "Filling in the setor is mandatory")]
        public string? Setor { get; set; }

        [Required(ErrorMessage = "a valid user id is required")]
        public string? AplicationUserId { get; set; }

    }
}
