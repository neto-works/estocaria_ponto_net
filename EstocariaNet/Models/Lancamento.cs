using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EstocariaNet.Models
{
    [Table("Lancamentos")]
    public class Lancamento
    {
        public Lancamento() {
            Relatorios = new Collection<Relatorio>();
        }

        public int LancamentoId { get; set; }

        public DateTime? Data { get; set; } = DateTime.Now;

        [Required]
        public float QuantEntrada { get; set; }

        [Required]
        public float QuantSaida { get; set; }

        [ForeignKey("Estoque")]
        public int? EstoqueaId { get; set; }

        [ForeignKey("Produto")]
        public int? ProdutoId { get; set; }

        [ForeignKey("Estoquista")]
        public string? EstoquistaId { get; set; }

        [JsonIgnore]
        public Estoque? LinkEstoque { get; set; }

        [JsonIgnore]
        public Produto? LinkProduto { get; set; }

        [JsonIgnore]
        public Estoquista? LinkEstoquista { get; set; }

        public ICollection<Relatorio> Relatorios { get; set; }
    }
}
