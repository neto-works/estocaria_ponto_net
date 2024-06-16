using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EstocariaNet.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public Produto() {
            Lancamentos = new Collection<Lancamento>();
        }

        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        public string? Descricao { get; set; }

        [Required]
        public decimal? Preco { get; set; }

        [Required]
        public string? ImagemUrl { get; set; }

        [Required]
        public float? QuantEstoqueMin { get; set; }

        [Required]
        public float? QuantEstoqueMax { get; set; }

        public float? Saldo { get; set; } = 0;

        public DateTime? DataCadastro { get; set; } = DateTime.Now;



        [ForeignKey("Categoria")]
        public int? CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? LinkCategoria { get; set; }

        [ForeignKey("Estoque")]
        public int? EstoqueId { get; set; }

        [JsonIgnore]
        public Estoque? LinkEstoque { get; set; }

        [JsonIgnore]
        public ICollection<Lancamento> Lancamentos { get; set; }
    }
}
