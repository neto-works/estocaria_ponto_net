using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstocariaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasServices _categoriasServices;
        public CategoriasController(ICategoriasServices categoriasServices) {
            _categoriasServices = categoriasServices;
        }


        [HttpPost]
        [Authorize(Policy ="QuemPuderAdinistrar")]
        public async Task<IActionResult> CreateCategoria(CreateCategoriaDTO categoria) {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var novaCategoria = await _categoriasServices.AdicionarAsync(categoria);
                return StatusCode(201,novaCategoria);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this,ex);
            }
        }

    }
}
