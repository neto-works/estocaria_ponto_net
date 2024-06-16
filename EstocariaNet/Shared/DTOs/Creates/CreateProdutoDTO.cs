using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs.Creates
{
    public class CreateProdutoDTO
    {
        [Required(ErrorMessage = "Filling in the product name is mandatory.")]
        [StringLength(100,ErrorMessage = "The maximum number of characters in the product name must not exceed 100 characters")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Filling in the product description is mandatory.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "The unit price of the product is mandatory.")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve ser entre 0.01 e 999999.99.")]
        public decimal? Preco { get; set; }

        [Required(ErrorMessage = "Filling in the product image url is mandatory.")]
        [StringLength(255, ErrorMessage = "The maximum number of characters in the product image url must not exceed 255 characters")]
        public string? ImagemUrl { get; set; }

        [Required(ErrorMessage = "The Minimum quantity allowed in stock must exist.")]
        [Range(0, float.MaxValue, ErrorMessage = "The minimum stock quantity must be a positive value.")]
        public float? QuantEstoqueMin { get; set; }
       
        [Required(ErrorMessage = "The maximum quantity allowed in stock must exist.")]
        [Range(0, float.MaxValue, ErrorMessage = "The max stock quantity must be a positive value.")]
        public float? QuantEstoqueMax { get; set; }


        //Options for default values in logig services
        public int? CategoriaId { get; set; } = null;
        public int? EstoqueId { get; set; } = null;

    }
}
