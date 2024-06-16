using EstocariaNet.Models;
using EstocariaNet.Shared.DTOs;

namespace EstocariaNet.Shared.Repositories.Interfaces
{
    public interface IRepositoryProdutosPaginate
    {
        Task<IEnumerable<Produto>> GetProdutosByPaginate(ProdutosParameters resourcesParameters);
    }
}