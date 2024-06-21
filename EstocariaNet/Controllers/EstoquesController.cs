using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstocariaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoquesController : ControllerBase
    {
        private readonly IEstoquesServices _estoqueServices;
        public EstoquesController(IEstoquesServices estoqueServices)
        {
            _estoqueServices = estoqueServices;
        }

        [Authorize(Policy ="QuemPuderAdministrar")]
        [HttpPost]
        public async Task<IActionResult> CreateEstoque([FromBody] CreateEstoqueDTO estoqueDto){
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Errors = errors });
                }

                var novo = await _estoqueServices.AdicionarAsync(estoqueDto);
                return StatusCode(201, novo);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize(Policy ="QuemPuderEstocar")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstoqueById(int id)
        {
            try
            {
                var Estoque = await _estoqueServices.BuscarAsync(id);
                if (Estoque == null)
                {
                    return NotFound();
                }
                return Ok(Estoque);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize(Policy ="QuemPuderAdministrar")]
        [HttpGet]
        public async Task<IActionResult> GetAllEstoques()
        {
            try
            {
                var estoques = await _estoqueServices.BuscarTodosAsync();
                return Ok(estoques);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize(Policy ="QuemPuderAdministrar")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstoque(int id, [FromBody] UpdateEstoqueDTO estoqueDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var estoqueAtualizado = await _estoqueServices.AlterarAsync(id, estoqueDTO);
                return Ok(estoqueAtualizado);
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
        public async Task<IActionResult> DeleteEstoque(int id)
        {
            try
            {
                _ = await _estoqueServices.ExcluirAsync(id);
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
