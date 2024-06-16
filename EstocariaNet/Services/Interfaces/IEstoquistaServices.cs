using EstocariaNet.Models;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;

namespace EstocariaNet.Services.Interfaces
{
    public interface IEstoquistaServices
    {
        Task<Estoquista> AdicionarAsync(CreateEstoquistaDTO estoquista);
        Task<Estoquista> AlterarAsync(string id, UpdateEstoquistaDTO estoquista);
        Task<Estoquista> ExcluirAsync(string id);
        Task<Estoquista> BuscarAsync(string id);
        Task<IEnumerable<Estoquista>> BuscarTodosAsync();
    }
}
