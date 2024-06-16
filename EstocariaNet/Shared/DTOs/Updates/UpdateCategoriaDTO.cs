using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs.Updates
{
    public class UpdateCategoriaDTO
    {
        public int? CategoriaId { get; set; } // Opcional, usado para identificar o produto a ser atualizado


        [StringLength(80, ErrorMessage = "The maximum number of characters in the categoria name must not exceed 100 characters")]
        public string? Nome { get; set; }
    }
}
