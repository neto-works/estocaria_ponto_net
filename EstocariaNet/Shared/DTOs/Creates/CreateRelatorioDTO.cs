namespace EstocariaNet.Shared.DTOs.Creates
{
    public class CreateRelatorioDTO
    {
        public string? RelatorioName { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public bool? PredicaoProxMeses { get; set; }
        public DateTime MesAnoPred {  get; set; }
        public string? AdminId { get; set; }
    }
}
