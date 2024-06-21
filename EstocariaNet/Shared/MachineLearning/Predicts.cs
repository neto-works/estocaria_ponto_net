using EstocariaNet.Models;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace EstocariaNet.Shared.MachineLearning
{
    public class ObjectResultPredict
    {
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
        public static ObjectResultPredict ExecutePredict(int mesPred, int produtoPred, IEnumerable<Lancamento> lancamentos)
        {
            MLContext context = new MLContext();
            // Converter lançamentos em modelos de entrada
            List<InputModels> dados = lancamentos.Select(l => new InputModels{ProdutoId = (int)l.ProdutoId!,Mes = l.Data!.Value.Month,Ano = l.Data!.Value.Year,Entradas = l.QuantEntrada,Saidas = l.QuantSaida}).ToList();
            IDataView dadosDeTreinamento = context.Data.LoadFromEnumerable(dados);

            // Definir pipeline de treinamento com características adicionais | transformar todos os dados em 1 só tipo -> pq se nn ele nn normaliza matriz certo
             var pipeline = context.Transforms.Conversion.ConvertType(new[] { new InputOutputColumnPair("ProdutoId"), new InputOutputColumnPair("Mes"), new InputOutputColumnPair("Ano"), new InputOutputColumnPair("Entradas") }, DataKind.Single)
            .Append(context.Transforms.Concatenate("Features", new[] { "ProdutoId", "Mes", "Ano", "Entradas" }))
            .Append(context.Regression.Trainers.Sdca(labelColumnName: "Saidas", maximumNumberOfIterations: 100));

            var model = pipeline.Fit(dadosDeTreinamento);
            var mecanismoDePrevisao = context.Model.CreatePredictionEngine<InputModels, ResultModels>(model);
            var input = new InputModels { ProdutoId = produtoPred, Mes = mesPred, Ano = DateTime.Now.Year, Entradas = 0 }; // Assume no entradas para a previsão
            var resultado = mecanismoDePrevisao.Predict(input);

            // Calcular precisão
            var metrics = context.Regression.Evaluate(model.Transform(dadosDeTreinamento), labelColumnName: "Saidas");
            // Retornar resultado com métricas
            return new ObjectResultPredict{Mes = mesPred,Resultado = resultado.VaoSair,R2 = (float)metrics.RSquared,MAError = (float)metrics.MeanAbsoluteError,MSError = (float)metrics.MeanSquaredError,RMSError = (float)metrics.RootMeanSquaredError,LossFunctionError = (float)metrics.LossFunction};
        }
    }
}

