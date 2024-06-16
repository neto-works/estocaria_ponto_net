using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Validate;
using Microsoft.AspNetCore.Mvc;

namespace EstocariaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        public AdminsController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(string id)
        {
            try
            {
                var admin = await _adminServices.BuscarAsync(id);
                if (admin is null)
                {
                    return NotFound();
                }
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            try
            {
                var admins = await _adminServices.BuscarTodosAsync();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(string id, [FromBody] UpdateAdminDTO adminDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var AdminAtualizado = await _adminServices.AlterarAsync(id, adminDTO);
                return Ok(AdminAtualizado);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            try
            {
                _ = await _adminServices.ExcluirAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

    }
}
