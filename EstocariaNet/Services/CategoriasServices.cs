using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;

namespace EstocariaNet.Services
{
    public class CategoriasServices : ICategoriasServices
    {
        private readonly IRepository<Categoria> _categoriaRepository;
        public CategoriasServices(IRepository<Categoria> categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<Categoria> AdicionarAsync(CreateCategoriaDTO createCategoria)
        {
            Categoria categoria = new Categoria { Nome = createCategoria.Nome };
            return await _categoriaRepository.CreateAsync(categoria);
        }

        public Task<Categoria> AlterarAsync(int id, UpdateCategoriaDTO categoria)
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> BuscarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Categoria>> BuscarTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> ExcluirAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
