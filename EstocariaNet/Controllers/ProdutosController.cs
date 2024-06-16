using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstocariaNet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosServices _produtosServices;
        public ProdutosController(IProdutosServices produtosServices)
        {
            _produtosServices = produtosServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduto([FromBody] CreateProdutoDTO produto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Errors = errors });
                }

                var novoProduto = await _produtosServices.AdicionarAsync(produto);
                return StatusCode(201, novoProduto);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize("QuemPuderEstocar")]
        [Authorize("QuemPuderAdinistrar")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoById(int id)
        {
            try
            {
                var produto = await _produtosServices.BuscarAsync(id);
                if (produto == null)
                {
                    return NotFound();
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize("QuemPuderEstocar")]
        [Authorize("QuemPuderAdinistrar")]
        [HttpGet]
        public async Task<IActionResult> GetAllProdutos()
        {
            try
            {
                var produtos = await _produtosServices.BuscarTodosAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize("QuemPuderAdinistrar")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] UpdateProdutoDTO produtoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var produtoAtualizado = await _produtosServices.AlterarAsync(id, produtoDTO);
                return Ok(produtoAtualizado);
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
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                _ = await _produtosServices.ExcluirAsync(id);
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

        [Authorize("QuemPuderEstocar")]
        [Authorize("QuemPuderAdinistrar")]
        [HttpGet("pagination")]
        public async Task<IActionResult> GetProdutos([FromQuery] ProdutosParameters resoucesParameters)
        { //from query faz a vinculaçãovaloresfornecisos a consulta
            try
            {
                var produtos = await _produtosServices.BuscarPorParametrosAsync(resoucesParameters);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return ServerErrorStandardized.Error500(this, ex);
            }
        }

        [Authorize("QuemPuderEstocar")]
        [Authorize("QuemPuderAdinistrar")]
        [HttpPost("{produto_id}/Categorias/{categoria_id}")]
        public async Task<IActionResult> AssociarCategoriaAProdutoAsync(int produtoId, int categoriaId)
        {
            try
            {
                var produtoAtualizado = await _produtosServices.AssociarCategoriaAProdutoAsync(produtoId, categoriaId);
                return Ok(produtoAtualizado);
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