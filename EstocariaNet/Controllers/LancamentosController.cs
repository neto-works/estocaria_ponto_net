using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EstocariaNet.Shared.Validate;
using EstocariaNet.Shared.DTOs;

namespace EstocariaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {
        private readonly ILancamentosServices _lancamentosService;
        public LancamentosController(ILancamentosServices lancamentosService)
        {
            _lancamentosService = lancamentosService;
        }

        [Authorize(Policy = "QuemPuderEstocar")]
        [HttpPost]
        public async Task<IActionResult> FazerNovoLancamento([FromBody] CreateLancamentoDTO lancamento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Errors = errors });
                }

                var novo = await _lancamentosService.RealizarNovoLancamentoAsync(lancamento);

                return Ok(novo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        
        [Authorize(Policy = "QuemPuderAdinistrar")]
        [HttpGet("Periodo")]
        public async Task<IActionResult> GetLancamentosPeriodo([FromBody] DataComaparableDTO datas)
        {
            try{
                if(!ModelState.IsValid){
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Errors = errors });
                }
                var lancamentos = await _lancamentosService.BuscarTodosInitDataToEndDataAsync(datas);
                return Ok(lancamentos);

            }catch(Exception ex){
                return ServerErrorStandardized.Error500(this,ex);
            }
        }
    }
}
