using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstocariaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatoriosServices _relatoriosService;
        public RelatoriosController(IRelatoriosServices relatoriosService)
        {
            _relatoriosService = relatoriosService;
        }


        [Authorize(Policy ="QuemPuderFazerAmbasAsFuncoes")]
        [HttpPost]
        public async Task<IActionResult> CriarRelatorio( CreateRelatorioDTO dadosCriacao){
            try{
                if(!ModelState.IsValid){
                    var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Errors = erros });
                }
                var relatorioCriado =  await _relatoriosService.CriarRelatorioAsync(dadosCriacao);
                return Ok(relatorioCriado);

            }catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception e){
                return ServerErrorStandardized.Error500(this,e);
;            }
        }

    }
}