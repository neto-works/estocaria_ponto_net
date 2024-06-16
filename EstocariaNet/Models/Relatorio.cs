using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstocariaNet.Models
{
    [Table("Relatorios")]
    public class Relatorio
    {
        public Relatorio() {
            Lancamentos = new Collection<Lancamento>();
        }

        [Key]
        public int RelatorioId {  get; set; }

        [Required]
        [StringLength(80)]
        public string? RelatorioName { get; set; }

        public DateTime? Data { get; set; } = DateTime.Now;

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int? ProdutoMaisSaiu { get; set; }
        public int? ProdutoMaisEntrou { get; set; }
        public double? TotalArrecadado { get; set; }

        public bool? PredicaoProxMeses { get; set; }
        public DateTime MesAnoPred {  get; set; }
        public int? PredProdutoSaida { get; set; }
        public int? PredProdutoEntrada { get; set; }
        public double? PredTotalArrecadar { get; set; }


        [ForeignKey("Admin")]
        public string? AdminId { get; set; }
        public Admin? LinkAdmin { get; set; }

        public ICollection<Lancamento> Lancamentos { get; set; }

    }
}
