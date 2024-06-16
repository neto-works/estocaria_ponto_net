using EstocariaNet.Models;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;

namespace EstocariaNet.Services.Interfaces
{
    public interface IAdminServices
    {
        Task<Admin> AdicionarAsync(CreateAdminDTO admin);
        Task<Admin> AlterarAsync(string id, UpdateAdminDTO admin);
        Task<Admin> ExcluirAsync(string id);
        Task<Admin> BuscarAsync(string id);
        Task<IEnumerable<Admin>> BuscarTodosAsync();

    }
}
