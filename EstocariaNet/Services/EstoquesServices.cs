using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;

namespace EstocariaNet.Services
{
    public class EstoquesServices : ManagerDTOS<Estoque, CreateEstoqueDTO, UpdateEstoqueDTO>, IEstoquesServices
    {
        private readonly IRepository<Estoque> _repositoryEstoque;
        public EstoquesServices(IRepository<Estoque> repositoryEstoque)
        {
            _repositoryEstoque = repositoryEstoque;
        }

        public async Task<Estoque> AdicionarAsync(CreateEstoqueDTO estoqueDto)
        {
            Estoque estoque = ConvertCreateDtoToClass(estoqueDto);
            return await _repositoryEstoque.CreateAsync(estoque);
        }

        public async Task<Estoque> AlterarAsync(int id, UpdateEstoqueDTO estoque)
        {
            Estoque? estoqueExists = await _repositoryEstoque.GetByIdAsync(e => e.EstoqueId == id);
            if (estoqueExists is null)
            {
                throw new ArgumentException($"Estoque com o ID {id} não encontrado.");
            }
            UpdateClassToDto(estoqueExists, estoque);
            return await _repositoryEstoque.UpdateAsync(estoqueExists);
        }

        public async Task<Estoque> BuscarAsync(int id)
        {
            Estoque? estoque = await _repositoryEstoque.GetByIdAsync(p => p.EstoqueId == id);
            if (estoque is null)
            {
                throw new ArgumentException($"Estoque com o ID {id} não encontrado.");
            }
            return estoque;
        }

        public async  Task<IEnumerable<Estoque>> BuscarTodosAsync()
        {
            return await _repositoryEstoque.GetAllAsync();
        }

        public async Task<Estoque> ExcluirAsync(int id)
        {
            Estoque? estoqueParaExcluir = await _repositoryEstoque.GetByIdAsync(p => p.EstoqueId == id);

            if (estoqueParaExcluir is null)
            {
                throw new ArgumentException($"Estoque com o ID {id} não encontrado.");
            }
            await _repositoryEstoque.DeleteAsync(id);
            return estoqueParaExcluir;
        }

        public async Task<bool> InitEstoque()
        {
            try
            {
                var estoquePadrao = await _repositoryEstoque.GetAllAsync();
                if (!estoquePadrao.Any())
                {
                    var est = await _repositoryEstoque.CreateAsync(new Estoque { Nome = "Padrão", Local = "Matriz", Capacidade = 500 });
                    if (est != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        protected override Estoque ConvertCreateDtoToClass(CreateEstoqueDTO estoqueDto)
        {
            return new Estoque { Nome = estoqueDto.Nome, Local = estoqueDto.Local, Capacidade = estoqueDto.Capacidade };
        }

        protected override void UpdateClassToDto(Estoque antigo, UpdateEstoqueDTO estoqueDto)
        {
            antigo.Nome = estoqueDto.Nome;
            antigo.Local = estoqueDto.Local;
            antigo.Capacidade = estoqueDto.Capacidade;
        }
    }
}
