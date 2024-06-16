using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstocariaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthServices authServices, ILogger<AuthController> logger)
        {
            _authServices = authServices;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var responseToken = await _authServices.LogarUser(model);
            if (responseToken != null)
            {
                return Ok(responseToken);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistroDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Errors = errors });
                }

                var responseCode = await _authServices.RegisterUser(model);

                if (responseCode.Status == "Error")
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, responseCode);
                }
                return Ok(responseCode);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDTO tokenModel)
        {
            var responseCode = await _authServices.ExpiredRefreshToken(tokenModel);
            if (responseCode is ResponseDTO)
            {
                return StatusCode(StatusCodes.Status400BadRequest, responseCode);
            }
            return responseCode;

        }

        [Authorize(Policy = "QuemPuderAdministrar")]
        [HttpPost]
        [Route("revoke/{email}")]
        public async Task<IActionResult> Revoke(string email)
        {
            var revogado = await _authServices.Revoke(email);
            return revogado is true ? NoContent() : BadRequest("Invalid email of user.");
        }

        [Authorize(Policy = "QuemPuderAdministrar")]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var response = await _authServices.CreateRole(roleName);
            if (response.Status == "Error")
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status200OK, response);

        }

        [Authorize(Policy = "QuemPuderAdministrar")]
        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var roleResponse = await _authServices.AddRoleForUser(email, roleName);
            if (roleResponse.Status == "Sucess")
            {
                _logger.LogInformation(1, $"User {email} added to the {roleName} role");
                return StatusCode(StatusCodes.Status200OK, roleResponse);

            }
            _logger.LogInformation(1, $"Error unable to add user");
            return BadRequest(new { error = $"Unable find user or user user already has this role " });
        }

    }
}
