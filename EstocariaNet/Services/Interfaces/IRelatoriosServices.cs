using EstocariaNet.Models;
using EstocariaNet.Shared.DTOs.Creates;

namespace EstocariaNet.Services.Interfaces
{
    public interface IRelatoriosServices
    {
        Task<Relatorio> CriarRelatorioAsync(CreateRelatorioDTO relatorio);
        Task<Relatorio> ExcluirAsync(string id);
        Task<Relatorio> BuscarAsync(string id);
        Task<IEnumerable<Relatorio>> BuscarTodosAsync();

    }
}
