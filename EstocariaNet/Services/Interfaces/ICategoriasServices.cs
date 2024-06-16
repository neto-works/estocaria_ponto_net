using EstocariaNet.Models;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;

namespace EstocariaNet.Services.Interfaces
{
    public interface ICategoriasServices
    {
        Task<Categoria> AdicionarAsync(CreateCategoriaDTO categoria);
        Task<Categoria> AlterarAsync(int id, UpdateCategoriaDTO categoria);
        Task<Categoria> ExcluirAsync(int id);
        Task<Categoria> BuscarAsync(int id);
        Task<IEnumerable<Categoria>> BuscarTodosAsync();
    }
}
