using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EstocariaNet.Models
{
    [Table("Estoques")]
    public class Estoque
    {
        public Estoque()
        {
            Produtos = new Collection<Produto>();
            Estoquistas = new Collection<Estoquista>();
            Lancamentos = new Collection<Lancamento>();
        }
        [Key]
        public int EstoqueId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(120)]
        public string? Local { get; set; }

        [Required]
        public float Capacidade { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;



        [ForeignKey("Admin")]
        public string? EstoqueAdminId { get; set; }

        [JsonIgnore]
        public Admin? LinkAdmin { get; set; }

        [JsonIgnore]
        public ICollection<Estoquista> Estoquistas { get; set; }

        [JsonIgnore]
        public ICollection<Produto> Produtos { get; set; }

        [JsonIgnore]
        public ICollection<Lancamento> Lancamentos { get; set; }
    }
}
