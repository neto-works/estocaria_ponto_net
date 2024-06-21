using EstocariaNet.Controllers;
using EstocariaNet.Models;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.DTOs.Creates;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Testes.Controllers
{
    [TestFixture]
    public class UnitTestControllerRelatorio
    {
        private Mock<IRelatoriosServices>? _relatoriosServices;
        private RelatoriosController? _controller;



        [SetUp]
        public void SetUp()
        {
            _relatoriosServices = new Mock<IRelatoriosServices>();
            _controller = new RelatoriosController(_relatoriosServices.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _relatoriosServices = null;
            _controller = null;
        }

        [Test]
        public async Task CreatedControllerMustReturnStatuscode200IfTheRequestisSuccessful()
        {
            var createRelatorioDTO = new CreateRelatorioDTO {};
            var relatorio = new Relatorio {};

            _relatoriosServices!.Setup(s => s.CriarRelatorioAsync(It.IsAny<CreateRelatorioDTO>())).ReturnsAsync(relatorio);
            var result = await _controller!.CriarRelatorio(createRelatorioDTO);


            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(relatorio));
        }

        [Test]
        public async Task CreateCategoriaShouldReturnStatusCode500WhenExceptionIsThrown()
        {
            var relatorio = new CreateRelatorioDTO();
            _relatoriosServices!.Setup(s => s.CriarRelatorioAsync(It.IsAny<CreateRelatorioDTO>())).ThrowsAsync(new Exception("Test Exception internal server error::putaria"));

            var result = await _controller!.CriarRelatorio(relatorio);

            Assert.That(result, Is.TypeOf<ObjectResult>());
            ObjectResult obj = (ObjectResult)result;

            var statusCodeResult = new StatusCodeResult(obj.StatusCode!.Value);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }


    }
}