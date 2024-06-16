using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs.Updates
{
    public class UpdateProdutoDTO
    {
        public int? ProdutoId { get; set; } // Opcional, usado para identificar o produto a ser atualizado

        [StringLength(100, ErrorMessage = "The maximum number of characters in the product name must not exceed 100 characters")]
        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        [Range(0.01, 999999.99, ErrorMessage = "O preço deve ser entre 0.01 e 999999.99.")]
        public decimal? Preco { get; set; }

        [StringLength(255, ErrorMessage = "The maximum number of characters in the product image url must not exceed 255 characters")]
        public string? ImagemUrl { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "The minimum stock quantity must be a positive value.")]
        public float? QuantEstoqueMin { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "The max stock quantity must be a positive value.")]
        public float? QuantEstoqueMax { get; set; }

        public int? CategoriaId { get; set; }

        public int? EstoqueId { get; set; }
    }
}
