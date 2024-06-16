using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs.Creates
{
    public class CreateCategoriaDTO
    {
        [Required(ErrorMessage = "Filling in the product name is mandatory.")]
        [StringLength(80, ErrorMessage = "The maximum number of characters in the categoria name must not exceed 80 characters")]
        public string? Nome { get; set; }
    }
}
