﻿using EstocariaNet.Services.Interfaces;
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

        [Authorize("QuemPuderEstocar")]
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

        [Authorize("QuemPuderAdinistrar")]
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

        [Authorize("QuemPuderAdinistrar")]
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

        [Authorize("QuemPuderAdinistrar")]
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
