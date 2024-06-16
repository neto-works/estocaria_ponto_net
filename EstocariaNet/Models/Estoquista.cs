using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstocariaNet.Models
{
    [Table("Estoquistas")]
    public class Estoquista
    {
        public Estoquista() {
            Lancamentos = new Collection<Lancamento>();
        }

        [Key]
        public string? EstoquistaId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(16)]
        public string? Cpf { get; set; }

        [Required]
        [StringLength(50)]
        public string? Celular { get; set; }
        public DateTime DataInicio { get; set; }

        [ForeignKey("AplicationUser")]
        public string? AplicationUserEstoquistaId { get; set; }
        public AplicationUser? AplicationUser { get; set; }

        [ForeignKey("Estoque")]
        public int ? EstoquistaEstoqueId { get; set; }
        public Estoque? Estoque { get; set; }

        public ICollection<Lancamento> Lancamentos { get; set; }
    }
}
