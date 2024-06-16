using EstocariaNet.Models;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;

namespace EstocariaNet.Services.Interfaces
{
    public interface IProdutosServices
    {
        Task<Produto> AdicionarAsync(CreateProdutoDTO produto);
        Task<Produto> AlterarAsync(int id, UpdateProdutoDTO produto);
        Task<Produto> ExcluirAsync(int id);
        Task<Produto> BuscarAsync(int id);
        Task<IEnumerable<Produto>> BuscarTodosAsync();
        Task<Produto> AssociarCategoriaAProdutoAsync(int produtoId, int categoriaId);

        Task<IEnumerable<Produto>> BuscarPorParametrosAsync(ProdutosParameters produtosParameters);


    }
}
