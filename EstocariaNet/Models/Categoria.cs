using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EstocariaNet.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public Categoria()
        {
            ProdutoLink = new Collection<Produto>();
        }

        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }



        [JsonIgnore]
        public ICollection<Produto> ProdutoLink { get; set; }
    }
}
