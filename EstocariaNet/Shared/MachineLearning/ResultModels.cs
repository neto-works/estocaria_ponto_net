using Microsoft.ML.Data;

namespace EstocariaNet.Shared.MachineLearning
{
    public class ResultModels
    {
        [ColumnName("Score")]
        public float VaoSair { get; set; }
    }
}
