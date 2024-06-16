using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;

namespace EstocariaNet.Services
{
    public class AdminsServices : IAdminServices
    {
        private readonly IRepository<Admin> _repositoryAdmin;
        public AdminsServices(IRepository<Admin> repositoryAdmin)
        {
            _repositoryAdmin = repositoryAdmin;
        }

        public async Task<Admin> AdicionarAsync(CreateAdminDTO admin)
        {
            Admin administrador = ConvertDtoToClass(admin);
            return await _repositoryAdmin.CreateAsync(administrador);
        }

        public async Task<Admin> AlterarAsync(string id, UpdateAdminDTO admin)
        {
            Admin? adminExists = await _repositoryAdmin.GetByIdAsync(a => a.AdminId == id);
            if (adminExists is null)
            {
                throw new ArgumentException($"Admin com o ID {id} não encontrado.");
            }
            // Atualizar as propriedades do produto existente com base nos dados fornecidos
            UpdateClassToDto(adminExists, admin);
            await _repositoryAdmin.UpdateAsync(adminExists);

            return adminExists;
        }

        public async Task<Admin> BuscarAsync(string id)
        {
            Admin? admin = await _repositoryAdmin.GetByIdAsync(a => a.AdminId == id);

            if (admin is null)
            {
                throw new ArgumentException($"Admin com o ID {id} não encontrado.");
            }

            return admin;
        }

        public async Task<IEnumerable<Admin>> BuscarTodosAsync()
        {
             return await _repositoryAdmin.GetAllAsync();
        }

        public async Task<Admin> ExcluirAsync(string id)
        {
            Admin? adminExcluuir = await _repositoryAdmin.GetByIdAsync(a => a.AdminId == id);

            if (adminExcluuir is null)
            {
                throw new ArgumentException($"Admin com o ID {id} não encontrado.");
            }
            await _repositoryAdmin.DeleteAsync(id);

            return adminExcluuir;
        }

        private Admin ConvertDtoToClass(CreateAdminDTO adm)
        {
            return new Admin { Setor = adm.Setor, AplicationUserAdminId = adm.AplicationUserId };
        }

        private void UpdateClassToDto(Admin antigo, UpdateAdminDTO admin){
            antigo.Setor = admin.Setor;
        }
    }
}
