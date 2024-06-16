using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs.Creates
{
    public class CreateEstoqueDTO
    {
        [Required(ErrorMessage = "A name for the stock is mandatory.")]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Location for stock is mandatory.")]
        [StringLength(120)]
        public string? Local { get; set; }

        [Required(ErrorMessage = "Capacity for stock is mandatory")]
        public float Capacidade { get; set; }

    }
}
