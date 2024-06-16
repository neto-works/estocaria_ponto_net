using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstocariaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoquistasController : ControllerBase
    {
        private readonly IEstoquistaServices _estoquistaServices;
        public EstoquistasController(IEstoquistaServices estoquistaServices)
        {
            _estoquistaServices = estoquistaServices;
        }

        [Authorize(Policy ="QuemPuderAdministrar")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstoquistaById(string id)
        {
            try
            {
                var estoquista = await _estoquistaServices.BuscarAsync(id);
                if (estoquista == null)
                {
                    return NotFound();
                }
                return Ok(estoquista);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize(Policy ="QuemPuderAdministrar")]
        [HttpGet]
        public async Task<IActionResult> GetAllEstoquistas()
        {
            try
            {
                var estoquistas = await _estoquistaServices.BuscarTodosAsync();
                return Ok(estoquistas);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize(Policy ="QuemPuderEstocar")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstoquista(string id, [FromBody] UpdateEstoquistaDTO estoquistaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var estoquistaAtualizado = await _estoquistaServices.AlterarAsync(id, estoquistaDTO);
                return Ok(estoquistaAtualizado);
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

        [Authorize(Policy ="QuemPuderAdministrar")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstoquista(string id)
        {
            try
            {
                _ = await _estoquistaServices.ExcluirAsync(id);
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
