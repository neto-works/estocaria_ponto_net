using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EstocariaNet.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly ITokenServices _tokenServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IEstoquistaServices _estoquistaService;
        private readonly IAdminServices _adminService;
        public AuthServices(ITokenServices tokenServices, UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEstoquistaServices estoquistaService, IAdminServices adminService)
        {
            _tokenServices = tokenServices;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _estoquistaService = estoquistaService;
            _adminService = adminService;
        }

        public async Task<JwtObjectResultDTO> LogarUser(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var autClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name,user.UserName!),
                    new Claim(ClaimTypes.Email,user.Email!),
                    new Claim("TipoUsuario", GetTipoUsuarioString(user.TipoUsuario)), //chaims não podem ser dinamicas, enum
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    autClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenServices.GenerateAccessToken(autClaims, _configuration);
                var refreshToken = _tokenServices.GenerateRefreshToken();
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokennValidityInMinutes);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokennValidityInMinutes);
                await _userManager.UpdateAsync(user);

                return new JwtObjectResultDTO { Token = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = refreshToken, Expiration = token.ValidTo };
            }
            return null;
        }

        public async Task<ResponseDTO> RegisterUser(RegistroDTO model)
        {
            ResponseDTO? roleCriada = null;
            IdentityRole? role = null;

            var userExists = await _userManager.FindByEmailAsync(model.Email!);
            if (userExists != null)
            {
                return new ResponseDTO { Status = "Error", Message = "User already Exists!" };
            };

            AplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                TipoUsuario = ParseEnum<TipoUsuarioEnum>(model.TipoUsuario!)
            };
            var result = await _userManager.CreateAsync(user, model.Password!);

            if (!result.Succeeded)
            {
                return new ResponseDTO { Status = "Error", Message = "User creation failed!" };
            };
            switch (user.TipoUsuario)
            {

                case TipoUsuarioEnum.ADMIN:
                    var adminSalvo = await _adminService.AdicionarAsync(new CreateAdminDTO { Setor = "Administrativo", AplicationUserId = user.Id });
                    role = await _roleManager.FindByNameAsync("Administrar");

                    if (role is null)
                    {
                        roleCriada = await this.CreateRole("Administrar");
                    }
                    _ = await this.AddRoleForUser(model.Email!, "Administrar");
                    break;

                case TipoUsuarioEnum.ESTOQUISTA:
                    var estoquistaSalvo = await _estoquistaService.AdicionarAsync(new CreateEstoquistaDTO { AplicationUserId = user.Id, EstoqueId = null, Cpf = "", Celular = "" });
                    role = await _roleManager.FindByNameAsync("Estocar");

                    if (role is null)
                    {
                        roleCriada = await this.CreateRole("Estocar");
                    }
                    _ = await this.AddRoleForUser(model.Email!, "Estocar");
                    break;

                case TipoUsuarioEnum.GERENTE:
                    //ainda não existe o tipo gerente
                    await _userManager.DeleteAsync(user);
                    return new ResponseDTO { Status = "Error", Message = "The manager user type has not yet been made available in the application!" };
                    //break;

                default:
                    await _userManager.DeleteAsync(user);
                    return new ResponseDTO { Status = "Error", Message = "User creation failed!" };
            };

            return new ResponseDTO { Status = "Sucess", Message = "User created sucessfully!" };
        }

        public async Task<dynamic> ExpiredRefreshToken(TokenDTO tokenModel)
        {
            if (tokenModel is null)
            {
                return new ResponseDTO { Status = "Sucess", Message = "Invalid client request" };
            }

            string? acessToken = tokenModel.AcessToken ?? throw new ArgumentNullException(nameof(tokenModel));
            string? refreshToken = tokenModel.RefreshsToken ?? throw new ArgumentException(nameof(tokenModel));

            var principal = _tokenServices.GetPrincipalFromExpiredToken(acessToken!, _configuration);
            if (principal == null)
            {
                return new ResponseDTO { Status = "Sucess", Message = "Invalid acess token/refresh token" };
            }

            string? username = principal.Identity!.Name;

            var user = await _userManager.FindByNameAsync(username!);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new ResponseDTO { Status = "Sucess", Message = "Invalid acess token/refresh token" };
            }
            var newAcessToken = _tokenServices.GenerateAccessToken(principal.Claims.ToList(), _configuration);
            var newRefreshToken = _tokenServices.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);
            return new ObjectResult(new { acessToken = new JwtSecurityTokenHandler().WriteToken(newAcessToken), refreshToken = newRefreshToken });
        }

        public async Task<bool> Revoke(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<ResponseDTO> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (roleResult.Succeeded)
                {
                    return new ResponseDTO { Status = "Sucess", Message = $"Role {roleName} added Successfuly" };
                }
                else
                {
                    return new ResponseDTO { Status = "Error", Message = $"Issue adding the new  {roleName} role" };
                }
            }
            return new ResponseDTO { Status = "Error", Message = $"Role Already Exists" };
        }

        public async Task<ResponseDTO> AddRoleForUser(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {

                    return new ResponseDTO { Status = "Sucess", Message = $"User {user.Email} added to the {roleName} role" };
                }
                else
                {
                    return new ResponseDTO { Status = "Error", Message = $"Error unable to add user {user.Email} to the {roleName} role" };
                }
            }
            return new ResponseDTO { Status = "Error", Message = "Unable find user" };
        }

        public static T ParseEnum<T>(string value) where T : struct, Enum
        {
            if (Enum.TryParse(value, true, out T result) && Enum.IsDefined(typeof(T), result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"'{value}' is not a valid value for type {typeof(T).Name}");
            }
        }

        /// <summary>
        /// Converts the value of the <see cref="TipoUsuarioEnum"/> enumeration into a string with the corresponding name.
        /// If the value is <c>null</c>, or if the name cannot be determined (which is unlikely in well-defined enumerations),
        /// returns an empty string.
        /// </summary>
        /// <param name="tipoUsuario">A nullable <see cref="TipoUsuarioEnum"/> value.</param>
        /// <returns>
        /// The name of the enumeration constant that corresponds to the value of <paramref name="tipoUsuario"/>.
        /// If <paramref name="tipoUsuario"/> is <c>null</c> or the name cannot be determined, returns an empty string.
        /// </returns>
        public static string GetTipoUsuarioString(TipoUsuarioEnum? tipoUsuario)
        {
            return tipoUsuario.HasValue ? Enum.GetName(typeof(TipoUsuarioEnum), tipoUsuario.Value) ?? "" : "";
        }


    }
}
