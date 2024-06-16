using System.Collections.ObjectModel;
using System.Data;
using System.Linq.Expressions;
using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;

namespace EstocariaNet.Services
{
    public class RelatoriosServices : IRelatoriosServices
    {
        private readonly IRepositoryRelatorios _repositoryRelatorios;
        private readonly ILancamentosServices _lancamentosServices;

        public RelatoriosServices(IRepositoryRelatorios repositoryRelatorios, ILancamentosServices lancamentosServices)
        {
            _repositoryRelatorios = repositoryRelatorios;
            _lancamentosServices = lancamentosServices;
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
            /*
            TODO
            - chamar todos  os metodos de matematica
            - montar relatorio e colocar no objeto 
            */
            relatorio.RelatorioName = relatorioDto.RelatorioName;
            relatorio.DataInicio = relatorioDto.DataInicio;
            relatorio.DataFim = relatorioDto.DataFim;
            relatorio.ProdutoMaisSaiu = CalcularProdutoMaisSaiu(ocorrencias);
            relatorio.TotalArrecadado = CalcularTotalArrecadado(lancamentos);
            relatorio.PredicaoProxMeses = relatorioDto.PredicaoProxMeses;
            //  TODO ... dividar pra conquistar se PredicaoProxMeses for false evitar calcular, assumir false por enquanto
            relatorio.MesAnoPred = relatorioDto.MesAnoPred;
            relatorio.PredProdutoSaida = null;
            relatorio.PredProdutoEntrada = null;
            relatorio.PredTotalArrecadar = null;
            relatorio.AdminId =relatorioDto.AdminId;

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
                if (!ocorrencias.ContainsKey(l.LinkProduto!.ProdutoId))
                {
                    ocorrencias.Add(l.LinkProduto.ProdutoId, l.QuantSaida);
                }
                else
                {
                    ocorrencias[l.LinkProduto.ProdutoId] = ocorrencias[l.LinkProduto.ProdutoId] += l.QuantSaida;
                }
            };
            return ocorrencias;
        }

        private double CalcularTotalArrecadado(IEnumerable<Lancamento> lancamentos)
        {
            double total = 0;
            foreach (Lancamento l in lancamentos)
            {
                total = total + ((double)l.QuantSaida * (double)l.LinkProduto?.Preco!);
            }
            return total;
        }
    }
}