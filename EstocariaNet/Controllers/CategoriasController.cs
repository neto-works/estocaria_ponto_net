﻿using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
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
        [Authorize("QuemPuderAdinistrar")]
        public async Task<IActionResult> CreateCategoria(CreateCategoriaDTO categoria) {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var novaCategoria = await _categoriasServices.AdicionarAsync(categoria);
                return Ok(novaCategoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

    }
}