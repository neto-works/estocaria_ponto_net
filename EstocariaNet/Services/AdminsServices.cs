using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EstocariaNet.Services
{
    public class AdminsServices : IAdminServices
    {
        private readonly IRepository<Admin> _repositoryAdmin;
        private readonly UserManager<AplicationUser> _userManager;
        public AdminsServices(IRepository<Admin> repositoryAdmin,UserManager<AplicationUser> userManager)
        {
            _repositoryAdmin = repositoryAdmin;
            _userManager = userManager;
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
                throw new ArgumentException($"Admin with this ID does not exist in the system.");
            }
            // Atualizar as propriedades do produto existente com base nos dados fornecidos
            UpdateClassToDto(adminExists, admin);
            await _repositoryAdmin.UpdateAsync(adminExists);

            return adminExists;
        }

        public async Task<Admin> BuscarAsync(string id)
        {
            Admin? admin = await _repositoryAdmin.GetByIdAsync(a => a.AdminId == id);
            return (admin is null)? throw new ArgumentException($"Admin with this ID does not exist in the system.") : admin;
        }

        public async Task<IEnumerable<Admin>> BuscarTodosAsync()
        {
             return await _repositoryAdmin.GetAllAsync();
        }

        //poderia retornar nada logo mais quando é serviço é bom ter como pegar id do excluido, poderaser util pra exclusão mutua
        public async Task<Admin> ExcluirAsync(string id)
        {
            Admin? adminExcluir = await _repositoryAdmin.GetByIdAsync(a => a.AdminId == id);

            if (adminExcluir is null)
            {
                throw new ArgumentException($"Admin with this ID does not exist in the system.");
            }

            var userCorrespondente = await _userManager.FindByIdAsync(adminExcluir.AplicationUserAdminId!);
            await _repositoryAdmin.DeleteAsync(id);
            await _userManager.DeleteAsync(userCorrespondente!);
            return adminExcluir;
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
