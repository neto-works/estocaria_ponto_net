using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EstocariaNet.Models
{
    [Table("Admins")]
    public class Admin
    {
        public Admin() {
            Relatorios = new Collection<Relatorio>();
        }

        [Key]
        public string? AdminId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(80)]
        public string? Setor { get; set; }

        [ForeignKey("AplicationUser")]
        public string? AplicationUserAdminId { get; set; }
        public AplicationUser? AplicationUser { get; set; }

        [JsonIgnore]
        public ICollection<Relatorio> Relatorios { get; set; }

        
    }
}
