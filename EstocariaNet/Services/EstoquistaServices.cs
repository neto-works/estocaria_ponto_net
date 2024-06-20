using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EstocariaNet.Services
{
    public class EstoquistaServices : IEstoquistaServices
    {
        private readonly IRepository<Estoquista> _repositoryEstoquista;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly IEstoquesServices _serviceEstoque;

        public EstoquistaServices(IRepository<Estoquista> repositoryEstoquista,UserManager<AplicationUser> userManager, IEstoquesServices serviceEstoque)
        {
            _repositoryEstoquista = repositoryEstoquista;
             _userManager = userManager;
            _serviceEstoque = serviceEstoque;
        }

        public async Task<Estoquista> AdicionarAsync(CreateEstoquistaDTO estoquistaDto)
        {
            Estoquista estoquista = ConvertDtoToClass(estoquistaDto);
            return await _repositoryEstoquista.CreateAsync(estoquista);
        }

        public async Task<Estoquista> AlterarAsync(string id, UpdateEstoquistaDTO estoquista)
        {
            Estoquista? estoquistaExists = await _repositoryEstoquista.GetByIdAsync(e => e.EstoquistaId == id);
            if (estoquistaExists is null)
            {
                throw new ArgumentException($"Estoquista with this ID does not exist in the system.");
            }
            UpdateClassToDto(estoquistaExists, estoquista);
            await _repositoryEstoquista.UpdateAsync(estoquistaExists);

            return estoquistaExists;
        }

        public async Task<Estoquista> BuscarAsync(string id)
        {
            Estoquista? estoquista = await _repositoryEstoquista.GetByIdAsync(e => e.EstoquistaId == id);

            if (estoquista is null)
            {
                // Lidar com o caso em que o produto não foi encontrado
                throw new ArgumentException($"Estoquista with this ID does not exist in the system.");
            }

            return estoquista;
        }

        public async Task<IEnumerable<Estoquista>> BuscarTodosAsync()
        {
           return await _repositoryEstoquista.GetAllAsync();
        }

        public async Task<Estoquista> ExcluirAsync(string id)
        {
            Estoquista? estoquistaExclud = await _repositoryEstoquista.GetByIdAsync(e => e.EstoquistaId == id);

            if (estoquistaExclud is null)
            {
                throw new ArgumentException($"Estoquista with this ID does not exist in the system.");
            }
            var userCorrespondente = await _userManager.FindByIdAsync(estoquistaExclud.AplicationUserEstoquistaId!);
            await _repositoryEstoquista.DeleteAsync(id);
            await _userManager.DeleteAsync(userCorrespondente!);
            return estoquistaExclud;
        }

        private Estoquista ConvertDtoToClass(CreateEstoquistaDTO estoquista)
        {
            return new Estoquista
            {
                Celular = estoquista.Celular,
                AplicationUserEstoquistaId = estoquista.AplicationUserId,
                Cpf = estoquista.Cpf,
                EstoquistaEstoqueId = estoquista.EstoqueId
            };
        }

        private void UpdateClassToDto(Estoquista antigo, UpdateEstoquistaDTO estoquista)
        {
            antigo.Celular = estoquista.Celular;
            antigo.EstoquistaEstoqueId = estoquista.EstoquistaEstoqueId;
        }
    }
}
