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
        private Mock<IProdutosServices>? _produtosServicesMock;
        private ProdutosController? _controller;
        private int _productId = 1233;

        [SetUp]
        public void Setup()
        {
            _produtosServicesMock = new Mock<IProdutosServices>();
            _controller = new ProdutosController(_produtosServicesMock.Object);
        }
        public void TearDown() // usando gabage collector
        {
            this._produtosServicesMock = null;
            this._controller = null;
        }

        [Test]
        public async Task CreateProduto_ValidDataReturns201()
        {
            var produto = new CreateProdutoDTO();
            _produtosServicesMock!.Setup(service => service.AdicionarAsync(It.IsAny<CreateProdutoDTO>())).ReturnsAsync(new Produto());

            var result = await _controller!.CreateProduto(produto);

            Assert.That(result, Is.TypeOf<ObjectResult>());
            ObjectResult obj = (ObjectResult)result;

            var statusCodeResult = new StatusCodeResult(obj.StatusCode!.Value);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public async Task GetProdutoById_ProductNotFoundReturnsNotFound()
        {
            var result = await _controller!.GetProdutoById(this._productId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task UpdateProduto_InvalidModelReturnsBadRequest()
        {
            // Arrange
            _controller!.ModelState.AddModelError("NomeDoCampo", "Mensagem de erro");

            // Act
            var result = await _controller.UpdateProduto(this._productId, new UpdateProdutoDTO());

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateProduto_ShouldValidModelReturnsOk()
        {
            // Arrange
            var produtoDTO = new UpdateProdutoDTO { /* preencher com dados válidos */ };
            _produtosServicesMock!.Setup(service => service.AlterarAsync(this._productId, produtoDTO)).ReturnsAsync(new Produto());

            // Act
            var result = await _controller!.UpdateProduto(this._productId, produtoDTO);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateProduto_ShouldReturningNotfoundWhentryingtoUpdateanObjectbyIdthatdoesnotExistintheDatabaseandThrowAnArgumentExceptionn()
        {
            _produtosServicesMock!.Setup(service => service.AlterarAsync(this._productId, It.IsAny<UpdateProdutoDTO>())).Throws(new ArgumentException());

            var result = await _controller!.UpdateProduto(this._productId, new UpdateProdutoDTO());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task DeleteProduto_ShouldReturnANocontentWhenPassingAValidIdAndExcludingTheProduct()
        {
            _produtosServicesMock!.Setup(service => service.ExcluirAsync(this._productId)).ReturnsAsync(new Produto());
            var result = await _controller!.DeleteProduto(this._productId);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteProduto_ShouldReturnNotfoundWhenReceivingIdThatDoesNotExistAndThrowAnArgumentException()
        {
            _produtosServicesMock!.Setup(service => service.ExcluirAsync(this._productId)).Throws(new ArgumentException());
            var result = await _controller!.DeleteProduto(this._productId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
    }
}
