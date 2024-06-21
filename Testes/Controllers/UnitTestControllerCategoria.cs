using EstocariaNet.Controllers;
using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Testes.Controllers
{
    [TestFixture]
    public class UnitTestControllerCategoria
    {
        private Mock<ICategoriasServices>? _categoriaSeviceMock;
        private CategoriasController? _controller;



        [SetUp]
        public void SetUp()
        {
            _categoriaSeviceMock = new Mock<ICategoriasServices>();
            _controller = new CategoriasController(_categoriaSeviceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _categoriaSeviceMock = null;
            _controller = null;
        }
        
        [Test]
        public async Task CreatedControllerMustReturnStatuscode201IfTheRequestisSuccessful(){
            var categoria = new CreateCategoriaDTO();
            _categoriaSeviceMock!.Setup(s => s.AdicionarAsync(It.IsAny<CreateCategoriaDTO>())).ReturnsAsync(new Categoria());

            var result = await _controller!.CreateCategoria(categoria);

            Assert.That(result, Is.TypeOf<ObjectResult>());
            ObjectResult obj = (ObjectResult)result;

            var statusCodeResult = new StatusCodeResult(obj.StatusCode!.Value);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public async Task CreateCategoriaShouldReturnStatusCode500WhenExceptionIsThrown()
        {
            var categoria = new CreateCategoriaDTO();
            _categoriaSeviceMock!.Setup(s => s.AdicionarAsync(It.IsAny<CreateCategoriaDTO>())).ThrowsAsync(new Exception("Test Exception internal server error::putaria"));
            var result = await _controller!.CreateCategoria(categoria);

            Assert.That(result, Is.TypeOf<ObjectResult>());
            ObjectResult obj = (ObjectResult)result;

            var statusCodeResult = new StatusCodeResult(obj.StatusCode!.Value);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }


    }
}