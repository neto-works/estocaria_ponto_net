using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;

namespace EstocariaNet.Services
{
    public class ProdutosServices : IProdutosServices
    {
        private readonly IRepository<Produto> _repositoryProdutos;
        private readonly IRepository<Categoria> _repositoryCategorias;
        private readonly IRepositoryProdutosPaginate _repositoryProdutosPaginados;


        public ProdutosServices(IRepository<Produto> repositoryProdutos, IRepository<Categoria> repositoryCategorias, IRepositoryProdutosPaginate repositoryProdutosPaginados)
        {
            _repositoryProdutos = repositoryProdutos;
            _repositoryCategorias = repositoryCategorias;
            _repositoryProdutosPaginados = repositoryProdutosPaginados;
        }

        public async Task<Produto> AdicionarAsync(CreateProdutoDTO produto)
        {
            Produto p = ConvertCreateDtoToClass(produto);
            return await _repositoryProdutos.CreateAsync(p);
        }

        public async Task<Produto> AlterarAsync(int id, UpdateProdutoDTO produto)
        {
            // Recuperar o produto existente com o ID fornecido
            Produto? produtoExistente = await _repositoryProdutos.GetByIdAsync(p => p.ProdutoId == id);

            if (produtoExistente == null)
            {
                // Lidar com o caso em que o produto não existe
                throw new ArgumentException($"Produto com o ID {id} não encontrado.");
            }

            // Atualizar as propriedades do produto existente com base nos dados fornecidos
            UpdateClassToDto(produtoExistente, produto);

            // Salvar as alterações
            await _repositoryProdutos.UpdateAsync(produtoExistente);

            return produtoExistente;
        }

        public async Task<Produto> BuscarAsync(int id)
        {
            // Utilizar o método GetByIdAsync do repositório para buscar o produto pelo ID
            Produto? produto = await _repositoryProdutos.GetByIdAsync(p => p.ProdutoId == id);

            if (produto == null)
            {
                // Lidar com o caso em que o produto não foi encontrado
                throw new ArgumentException($"Produto com o ID {id} não encontrado.");
            }

            return produto;
        }

        public async Task<IEnumerable<Produto>> BuscarTodosAsync()
        {
            return await _repositoryProdutos.GetAllAsync();
        }

        public async Task<Produto> ExcluirAsync(int id)
        {
            Produto? produtoParaExcluir = await _repositoryProdutos.GetByIdAsync(p => p.ProdutoId == id);

            if (produtoParaExcluir == null)
            {
                // Lidar com o caso em que o produto não foi encontrado
                throw new ArgumentException($"Produto com o ID {id} não encontrado.");
            }

            // Excluir o produto utilizando o método DeleteAsync do repositório
            await _repositoryProdutos.DeleteAsync(id);

            return produtoParaExcluir;
        }

        public async Task<Produto> AssociarCategoriaAProdutoAsync(int produtoId, int categoriaId)
        {
            // Recuperar o produto existente com o ID fornecido
            Produto? produtoExistente = await _repositoryProdutos.GetByIdAsync(p => p.ProdutoId == produtoId);
            Categoria? categoriaExistente = await _repositoryCategorias.GetByIdAsync(p => p.CategoriaId == categoriaId);

            if (produtoExistente == null || categoriaExistente == null)
            {
                // Lidar com o caso em que o produto não existe
                throw new ArgumentException($"Produto com o ID {produtoId} não encontrado ou essa Categoria não {categoriaId} existe.");
            }

            // Atualizar as propriedades do produto existente com base nos dados fornecidos
            produtoExistente.CategoriaId = categoriaId;

            // Salvar as alterações
            await _repositoryProdutos.UpdateAsync(produtoExistente);

            return produtoExistente;

        }
        public async Task<IEnumerable<Produto>> BuscarPorParametrosAsync(ProdutosParameters produtosParameters)
        {
            return await _repositoryProdutosPaginados.GetProdutosByPaginate(produtosParameters);
        }

        private Produto ConvertCreateDtoToClass(CreateProdutoDTO produto)
        {
            return new Produto
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemUrl = produto.ImagemUrl,
                QuantEstoqueMin = produto.QuantEstoqueMin,
                QuantEstoqueMax = produto.QuantEstoqueMax,
                EstoqueId = (produto.EstoqueId != null) ? produto.EstoqueId : 1,
                CategoriaId = (produto.CategoriaId != null) ? produto.CategoriaId : null,
            };
        }

        private void UpdateClassToDto(Produto antigo, UpdateProdutoDTO produto)
        {
            antigo.Nome = produto.Nome;
            antigo.Descricao = produto.Descricao;
            antigo.Preco = produto.Preco;
            antigo.ImagemUrl = produto.ImagemUrl;
            antigo.QuantEstoqueMin = produto.QuantEstoqueMin;
            antigo.QuantEstoqueMax = produto.QuantEstoqueMax;
            antigo.CategoriaId = produto.CategoriaId ?? antigo.CategoriaId;
            antigo.EstoqueId = produto.EstoqueId ?? antigo.EstoqueId;
        }

    }
}

