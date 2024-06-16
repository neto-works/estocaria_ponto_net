using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;

namespace EstocariaNet.Services
{
    public class EstoquesServices : IEstoquesServices
    {
        private readonly IRepository<Estoque> _repositoryEstoque;
        public EstoquesServices(IRepository<Estoque> repositoryEstoque) {
            _repositoryEstoque = repositoryEstoque;
        }

        public Task<Estoque> AdicionarAsync(CreateEstoqueDTO estoque)
        {
            throw new NotImplementedException();
        }

        public Task<Estoque> AlterarAsync(int id, UpdateEstoqueDTO estoque)
        {
            throw new NotImplementedException();
        }

        public Task<Estoque> BuscarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Estoque>> BuscarTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Estoque> ExcluirAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InitEstoque() {

            try {
                var estoquePadrao = await _repositoryEstoque.GetAllAsync();
                if (!estoquePadrao.Any())
                {
                    var est = await _repositoryEstoque.CreateAsync(new Estoque { Nome = "Padrão", Local = "Matriz", Capacidade = 500 });
                    if(est!=null) {
                        return true;
                    }
                } 
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }
    }
}
