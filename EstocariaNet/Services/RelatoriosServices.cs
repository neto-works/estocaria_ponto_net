using System.Linq.Expressions;
using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.MachineLearning;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace EstocariaNet.Services
{
    public class RelatoriosServices : IRelatoriosServices
    {
        private readonly IRepositoryRelatorios _repositoryRelatorios;
        private readonly ILancamentosServices _lancamentosServices;
        private readonly IProdutosServices _produtosServices;

        public RelatoriosServices(IRepositoryRelatorios repositoryRelatorios, ILancamentosServices lancamentosServices, IProdutosServices produtosServices)
        {
            _repositoryRelatorios = repositoryRelatorios;
            _lancamentosServices = lancamentosServices;
            _produtosServices = produtosServices;
        }

        public Task<Relatorio> BuscarAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Relatorio>> BuscarTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Relatorio> ExcluirAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Relatorio>> BuscarTodosInitDataToEndDataAsync(DateTime dataInicial, DateTime dataFinal)
        {
            Expression<Func<Relatorio, bool>> predicate = relatorio => relatorio.Data >= dataInicial && relatorio.Data <= dataFinal;
            return await _repositoryRelatorios.GetRelatoriosInitDataToEndDataAsync(predicate);
        }

        public async Task<Relatorio> CriarRelatorioAsync(CreateRelatorioDTO relatorioDto)
        {
            DataComaparableDTO datas = new DataComaparableDTO { DataInicio = relatorioDto.DataInicio, DataFim = relatorioDto.DataFim };
            var lancamentos = await _lancamentosServices.BuscarTodosInitDataToEndDataAsync(datas);

            if (lancamentos.IsNullOrEmpty())
            {
                throw new ArgumentException($"There are no releases created during this period, from {relatorioDto.DataInicio} to {relatorioDto.DataFim}.");
            }
            Dictionary<int, float> ocorrencias = this.CalcularOcorrenciasSaidas(lancamentos);
            Relatorio relatorio = new Relatorio();
            relatorio.RelatorioName = relatorioDto.RelatorioName;
            relatorio.DataInicio = relatorioDto.DataInicio;
            relatorio.DataFim = relatorioDto.DataFim;
            relatorio.ProdutoMaisSaiu = CalcularProdutoMaisSaiu(ocorrencias);
            relatorio.TotalArrecadado = Math.Round(await CalcularTotalArrecadado(lancamentos),2);
            relatorio.PredicaoProxMeses = relatorioDto.PredicaoProxMeses;
            relatorio.MesAnoPred = relatorioDto.MesAnoPred;

            ObjectResultPredict op = Predicts.ExecutePredict(3, 1, lancamentos);
            relatorio.PredProdutoSaida = (int)op.Resultado;
            relatorio.PredProdutoEntrada = null;
            relatorio.PredTotalArrecadar = null;
            relatorio.AdminId = relatorioDto.AdminId;

            return await _repositoryRelatorios.CreateAsync(relatorio);
        }
        private int CalcularProdutoMaisSaiu(Dictionary<int, float> ocorrencias)
        {
            int chaveMaiorValor = 0;
            float maior = 0;
            foreach (var produto in ocorrencias)
            {
                if (produto.Value > maior)
                {
                    maior = produto.Value;
                    chaveMaiorValor = produto.Key;
                }
            };
            return chaveMaiorValor;
        }

        private Dictionary<int, float> CalcularOcorrenciasSaidas(IEnumerable<Lancamento> lancamentos)
        {
            //id  -  saidas
            Dictionary<int, float> ocorrencias = new Dictionary<int, float>();

            foreach (Lancamento l in lancamentos)
            {
                if (!ocorrencias.ContainsKey((int)l.ProdutoId!))
                {
                    ocorrencias.Add((int)l.ProdutoId, l.QuantSaida);
                }
                else
                {
                    ocorrencias[(int)l.ProdutoId] = ocorrencias[(int)l.ProdutoId] += l.QuantSaida;
                }
            };
            return ocorrencias;
        }

        private async Task<double> CalcularTotalArrecadado(IEnumerable<Lancamento> lancamentos)
        {
            IEnumerable<Produto> listaDeProdutos = await _produtosServices.BuscarTodosAsync();
            double total = 0;
            if (listaDeProdutos != null && listaDeProdutos.Any())
            {
                foreach (Lancamento l in lancamentos)
                {
                    foreach (Produto p in listaDeProdutos)
                    {
                        if (l.ProdutoId == p.ProdutoId)
                        {
                            total = total + ((double)l.QuantSaida * (double)p.Preco!);
                        }
                    }
                }
            }

            return total;
        }
    }
}