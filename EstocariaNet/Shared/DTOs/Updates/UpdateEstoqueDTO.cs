using EstocariaNet.Models;

namespace EstocariaNet.Shared.DTOs.Updates
{
    public class UpdateEstoqueDTO
    {
        public string? Nome { get; set; }
        public string? Local { get; set; }
        public float Capacidade { get; set; }

    }
}