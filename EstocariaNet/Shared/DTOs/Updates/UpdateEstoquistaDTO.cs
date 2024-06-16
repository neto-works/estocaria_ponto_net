using EstocariaNet.Models;

namespace EstocariaNet.Shared.DTOs.Updates
{
    public class UpdateEstoquistaDTO
    {
        
        public string? AplicationUserId { get; set; }

        
        public string? Cpf { get; set; }

        
        public string? Celular { get; set; }

        public DateTime? DataInicio { get; set; }

        public AplicationUser? AplicationUser { get; set; }

        public int? EstoqueId { get; set; }
        public int? EstoquistaEstoqueId { get; set; }

        public Estoque? Estoque { get; set; }

        public UpdateEstoquistaDTO() { DataInicio = DateTime.Now; }
        
    }
}