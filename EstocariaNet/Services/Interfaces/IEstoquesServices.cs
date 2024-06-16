using EstocariaNet.Models;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;

namespace EstocariaNet.Services.Interfaces
{
    public interface IEstoquesServices
    {
        Task<Estoque> AdicionarAsync(CreateEstoqueDTO estoque);
        Task<Estoque> AlterarAsync(int id, UpdateEstoqueDTO estoque);
        Task<Estoque> ExcluirAsync(int id);
        Task<Estoque> BuscarAsync(int id);
        Task<IEnumerable<Estoque>> BuscarTodosAsync();
        Task<bool> InitEstoque();
    }
}
