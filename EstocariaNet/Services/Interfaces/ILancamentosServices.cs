using EstocariaNet.Models;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;

namespace EstocariaNet.Services.Interfaces
{
    public interface ILancamentosServices
    {
        Task<Lancamento> RealizarNovoLancamentoAsync(CreateLancamentoDTO lancamento);
        Task<Lancamento> AlterarLancamentoAsync(int id, UpdateLancamentoDTO lancamento);
        Task<Lancamento> ExcluirLancamentoAsync(int id);
        Task<Lancamento> BuscarLancamentoAsync(int id);
        Task<IEnumerable<Lancamento>> BuscarTodosAsync();
        Task<IEnumerable<Lancamento>> BuscarTodosInitDataToEndDataAsync(DataComaparableDTO datas);
    }
}
