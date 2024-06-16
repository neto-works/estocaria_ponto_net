using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;

namespace EstocariaNet.Services
{
    public class LancamentosServices : ILancamentosServices
    {
        private readonly IRepositoryLancamentos _repositoryLancamentos;
        private readonly IRepository<Produto> _repositoryProdutos;
        public LancamentosServices(IRepositoryLancamentos repositoryLancamentos, IRepository<Produto> repositoryProdutos)
        {
            _repositoryLancamentos = repositoryLancamentos;
            _repositoryProdutos = repositoryProdutos;
        }

        public async Task<Lancamento> RealizarNovoLancamentoAsync(CreateLancamentoDTO lancamento)
        {
            Lancamento lc = this.ConvertCreateDtoToClass(lancamento);
            Produto? produtoAtualizarSaldo = await _repositoryProdutos.GetByIdAsync(p => p.ProdutoId == lc.ProdutoId);

            if (produtoAtualizarSaldo == null)
            {
                throw new ArgumentException($"Produto com o ID {lc.ProdutoId} não encontrado.");

            }
            produtoAtualizarSaldo = this.AtualizarSaldo(produtoAtualizarSaldo, lc);

            _ = await _repositoryProdutos.UpdateAsync(produtoAtualizarSaldo!);

            return await _repositoryLancamentos.CreateAsync(lc);
        }

        public Task<Lancamento> AlterarLancamentoAsync(int id, UpdateLancamentoDTO lancamento)
        {
            throw new NotImplementedException();
        }

        public Task<Lancamento> BuscarLancamentoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lancamento>> BuscarTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Lancamento> ExcluirLancamentoAsync(int id)
        {
            throw new NotImplementedException();
        }

        private Lancamento ConvertCreateDtoToClass(CreateLancamentoDTO lancamento)
        {
            return new Lancamento
            {
                QuantEntrada = lancamento.QuantEntrada,
                QuantSaida = lancamento.QuantSaida,
                EstoqueaId = lancamento.EstoqueaId,
                ProdutoId = lancamento.ProdutoId,
                EstoquistaId = lancamento.EstoquistaId,
            };

        }
        private Produto AtualizarSaldo(Produto produto, Lancamento lancamento)
        {
            float result;

            if (float.TryParse(lancamento.QuantEntrada.ToString(),out result) && float.TryParse(lancamento.QuantSaida.ToString(), out result)) {

                float? calcAcresc = produto.Saldo - (lancamento.QuantEntrada - lancamento.QuantSaida);
                float? calcDecrement = produto.Saldo - (lancamento.QuantSaida - lancamento.QuantEntrada);
                float entrada = lancamento.QuantEntrada - lancamento.QuantSaida;
                float saida = lancamento.QuantSaida - lancamento.QuantEntrada;

                // quant entrada | saldo deve ser <= maxPermitido | não pode estourar a quant permitida estoque padrão
                if ((lancamento.QuantEntrada > lancamento.QuantSaida) && ((calcAcresc + produto.Saldo) < produto.QuantEstoqueMax) && ((entrada + produto.Saldo) <= 500) )
                {
                    produto.Saldo += entrada;
                }
                else if ((lancamento.QuantEntrada < lancamento.QuantSaida) && ((calcDecrement - produto.Saldo) >= 0)) {
                    produto.Saldo -= saida;
                }
                //pode eliminar essa lancamento.QuantEntrada == lancamento.QuantSaida
                else
                {
                    throw new Exception("Error.");
                }
               
            }
            else
            {
                throw new Exception("values type not is numbers.");
            }
            return produto;
        }

        public async Task<IEnumerable<Lancamento>> BuscarTodosInitDataToEndDataAsync(DataComaparableDTO datas)
        {
            return await _repositoryLancamentos.GetLancamentosInitDataToEndDataAsync(lancamento => lancamento.Data >= datas.DataInicio && lancamento.Data <= datas.DataFim);
        }
    }
}
