using EstocariaNet.Models;
using EstocariaNet.Services;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.Repositories.Interfaces;
using Moq;

namespace Testes.Services
{
    public class UnitTestServiceCategoria
    {
        private Mock<IRepository<Categoria>>? _categoriaRepositoryMock;
        private CategoriasServices? _categoriasServices;

        [SetUp]
        public void SetUp()
        {
            _categoriaRepositoryMock = new Mock<IRepository<Categoria>>();
            _categoriasServices = new CategoriasServices(_categoriaRepositoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _categoriaRepositoryMock = null;
            _categoriasServices = null;
        }

        [Test]
        public async Task AdicionarAsyncShouldReturnCategoriaWhenCreateCategoriaDTOIsValid()
        {
            var createCategoriaDTO = new CreateCategoriaDTO { Nome = "TestCategoria" };
            var categoria = new Categoria { Nome = "TestCategoria" };
            _categoriaRepositoryMock!.Setup(r => r.CreateAsync(It.IsAny<Categoria>())).ReturnsAsync(categoria);

            var result = await _categoriasServices!.AdicionarAsync(createCategoriaDTO);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Nome, Is.EqualTo(createCategoriaDTO.Nome));

            _categoriaRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Categoria>()), Times.Once);
        }

    }
}