using EstocariaNet.Models;
using Microsoft.ML;

namespace EstocariaNet.Shared.MachineLearning
{
    public class ObjectResultPredict {
        public int Mes { get; set; }
         public float Resultado { get; set; }
        public float R2 { get; set; }
        public float MAError { get; set; }
        public float MSError { get; set; }
        public float RMSError { get; set; }
        public float LossFunctionError { get; set; }

    }
    public class Predicts
    {
        public static ObjectResultPredict ExecutePredict(int mesPred,int produtoPred,IEnumerable<Lancamento> lancamentos)
        {
            MLContext context = new MLContext();

            List<InputModels> dados = new List<InputModels>();
            foreach (var lancamento in lancamentos){
                dados.Add(new InputModels {ProdutoId = (int)lancamento.ProdutoId!, Mes = lancamento.Data!.Value.Month, Ano = lancamento.Data!.Value.Year,Entrada = lancamento.QuantEntrada, Saida = lancamento.QuantSaida});
            }
            IDataView dadosDeTreinamento = context.Data.LoadFromEnumerable(dados);
                                                      
            var estimador = context.Transforms.Concatenate("Features", new[] { "Saidas" });
            var pipeline = estimador.Append(context.Regression.Trainers.Sdca(labelColumnName:"Saidas",maximumNumberOfIterations:100));
            var modelTrainee = pipeline.Fit(dadosDeTreinamento);

            var mecanismoDePrevisao = context.Model.CreatePredictionEngine<InputModels,ResultModels>(modelTrainee);

            var mesSaidas = new InputModels { ProdutoId=produtoPred, Saida = mesPred};

            var resultado = mecanismoDePrevisao.Predict(mesSaidas);

            //calcular precisao
            var testDataView = context.Data.LoadFromEnumerable(dados);
            var metrics = context.Regression.Evaluate(modelTrainee.Transform(testDataView), labelColumnName: "Saidas");

            return new ObjectResultPredict{Mes = mesSaidas.Mes,Resultado = resultado.VaoSair,R2=(float)metrics.RSquared,MAError=(float)metrics.MeanAbsoluteError,MSError=(float)metrics.MeanSquaredError,RMSError=(float)metrics.RootMeanSquaredError,LossFunctionError=(float)metrics.LossFunction};
        }
    }
}

