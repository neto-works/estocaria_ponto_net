using EstocariaNet.Controller;
using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Testes
{
    [TestFixture]
    public class UnitTestControllerProdutos
    {
        private Mock<IProdutosServices> _produtosServicesMock;
        private ProdutosController _controller;

        [SetUp]
        public void Setup()
        {
            _produtosServicesMock = new Mock<IProdutosServices>();
            _controller = new ProdutosController(_produtosServicesMock.Object);
        }

        [TearDown]
        public void TearDown() //usando gabage collector
        {
            _produtosServicesMock = null;
            _controller = null;
        }

        // [Test]
        // public async Task CreateProduto_ValidData_Returns201()
        // {
        //     var produto = new CreateProdutoDTO();
        //     _produtosServicesMock.Setup(service => service.AdicionarAsync(It.IsAny<CreateProdutoDTO>())).ReturnsAsync(new Produto());

        //     var result = await _controller.CreateProduto(produto);

        //     Assert.That(result, Is.TypeOf<IActionResult>());
        //     var statusCodeResult = (StatusCodeResult)result;
        //     Assert.That(statusCodeResult.StatusCode, Is.EqualTo(201));
        // }

        [Test]
        public async Task GetProdutoById_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            int productId = 1235;
            _ = _produtosServicesMock.Setup(service => service.BuscarAsync(It.IsAny<int>())).ReturnsAsync((Produto)null);

            // Act
            var result = await _controller.GetProdutoById(productId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task UpdateProduto_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            int productId = 123;
            _controller.ModelState.AddModelError("NomeDoCampo", "Mensagem de erro");

            // Act
            var result = await _controller.UpdateProduto(productId, new EstocariaNet.Shared.DTOs.Updates.UpdateProdutoDTO());

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateProduto_ValidModel_ReturnsOk()
        {
            // Arrange
            int productId = 123;
            var produtoDTO = new UpdateProdutoDTO { /* preencher com dados válidos */ };
            _produtosServicesMock.Setup(service => service.AlterarAsync(productId, produtoDTO)).ReturnsAsync(new Produto());

            // Act
            var result = await _controller.UpdateProduto(productId, produtoDTO);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateProduto_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            int productId = 123;
            _produtosServicesMock.Setup(service => service.AlterarAsync(productId, It.IsAny<UpdateProdutoDTO>())).Throws(new ArgumentException());

            // Act
            var result = await _controller.UpdateProduto(productId, new UpdateProdutoDTO());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task DeleteProduto_ValidId_ReturnsOk()
        {
            // Arrange
            int productId = 123;
            _produtosServicesMock.Setup(service => service.ExcluirAsync(productId)).ReturnsAsync(new Produto());

            // Act
            var result = await _controller.DeleteProduto(productId);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteProduto_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            int productId = 123;
            _produtosServicesMock.Setup(service => service.ExcluirAsync(productId)).Throws(new ArgumentException());

            // Act
            var result = await _controller.DeleteProduto(productId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
    }
}
