using EstocariaNet.Shared.DTOs;

namespace EstocariaNet.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<JwtObjectResultDTO> LogarUser(LoginDTO model);
        Task<ResponseDTO> RegisterUser(RegistroDTO model);
        Task<dynamic> ExpiredRefreshToken(TokenDTO tokenModel);
        Task<bool> Revoke(string email);
        Task<ResponseDTO> CreateRole(string roleName);
        Task<ResponseDTO> AddRoleForUser(string email, string roleName);
    }
}
