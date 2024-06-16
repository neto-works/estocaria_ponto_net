namespace EstocariaNet.Shared.DTOs.Creates
{
    public class CreateLancamentoDTO
    {
        public float QuantEntrada { get; set; }

        public float QuantSaida { get; set; }

        public int? EstoqueaId { get; set; }

        public int? ProdutoId { get; set; }

        public string? EstoquistaId { get; set; }


    }
}
