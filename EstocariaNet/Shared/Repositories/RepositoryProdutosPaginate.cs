using System.Linq.Expressions;
using EstocariaNet.Models;
using EstocariaNet.Shared.Context;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstocariaNet.Shared.Repositories
{
    public class RepositoryProdutosPaginate:IRepositoryProdutosPaginate
    {
        protected readonly AppDbContext _context;
        public RepositoryProdutosPaginate(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetProdutosByPaginate(ProdutosParameters resourcesParameters)
        {
            if (_context.Produtos == null)
            {
                return Enumerable.Empty<Produto>(); // Retorna uma lista vazia se não houver produtos no contexto
            }

            return await _context.Produtos.AsNoTracking()
            .OrderBy(p => p.ProdutoId).Skip((resourcesParameters.PageNumber - 1) * resourcesParameters.PageSize)
                .Take(resourcesParameters.PageSize).ToListAsync();
        }


    }
}