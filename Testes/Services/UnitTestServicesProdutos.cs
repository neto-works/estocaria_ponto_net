using System.Linq.Expressions;
using System.Reflection;
using EstocariaNet.Models;
using EstocariaNet.Services;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs.Updates;
using EstocariaNet.Shared.Repositories.Interfaces;
using Moq;

namespace Testes.Services
{
    [TestFixture]
    public class UnitTestServicesProdutos
    {
        private ProdutosServices? _produtosServices;
        private Mock<IRepository<Produto>>? _repositoryProdutos;
        private Mock<IRepository<Categoria>>? _repositoryCategorias;
        private Mock<IRepositoryProdutosPaginate>? _repositoryProdutosPaginados;

        [SetUp]
        public void Setup()
        {
            _repositoryProdutos = new Mock<IRepository<Produto>>();
            _repositoryCategorias = new Mock<IRepository<Categoria>>();
            _repositoryProdutosPaginados = new Mock<IRepositoryProdutosPaginate>();
            _produtosServices = new ProdutosServices(_repositoryProdutos.Object, _repositoryCategorias.Object, _repositoryProdutosPaginados.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _produtosServices = null;
        }

        public static bool CompararPropriedades(object objeto1, object objeto2)
        {
            if (objeto1 == null && objeto2 == null)
                return true;
            if (objeto1 == null || objeto2 == null)
                return false;
            Type tipo1 = objeto1.GetType();
            Type tipo2 = objeto2.GetType();
            if (tipo1 != tipo2)
                return false;

            PropertyInfo[] propriedades = tipo1.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propriedade in propriedades)
            {
                object? valor1 = propriedade.GetValue(objeto1);
                object? valor2 = propriedade.GetValue(objeto2);
                if (valor1 == null || valor2 == null)
                    return false;
                if (!valor1.Equals(valor2))
                    return false;
            }
            return true;
        }

        [Test]
        public void ShouldReturnNewProdutoWhenActionConvertCreateDtoToClass()
        {
            CreateProdutoDTO produtoDto = new CreateProdutoDTO();

            var methodInfo = typeof(ProdutosServices).GetMethod("ConvertCreateDtoToClass", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(methodInfo, Is.Not.Null);

            var result = (Produto?)methodInfo.Invoke(_produtosServices, new object[] { produtoDto });
            Assert.That(result, Is.InstanceOf<Produto>());
        }

        [Test]
        public void ShouldReturnUpdateClassToDtoIsProductModified()
        {
            Produto produto = new Produto{ProdutoId = 1,Nome = "Arroz",Descricao = "",Preco = 10,ImagemUrl = "",QuantEstoqueMin = 0,QuantEstoqueMax = 3,Saldo = 20,CategoriaId = null,EstoqueId = 1};
            Produto produtoCopia = new Produto { ProdutoId = 1, Nome = "Arroz", Descricao = "", Preco = 10, ImagemUrl = "", QuantEstoqueMin = 0, QuantEstoqueMax = 3, Saldo = 20, CategoriaId = null, EstoqueId = 1 };
            UpdateProdutoDTO produtoDto = new UpdateProdutoDTO { ProdutoId = 1, Nome = "Arroz do zé", Descricao = "", Preco = 10, ImagemUrl = "", QuantEstoqueMin = 1, QuantEstoqueMax = 345, CategoriaId = 1, EstoqueId = 1 };

            var methodInfo = typeof(ProdutosServices).GetMethod("UpdateClassToDto", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(methodInfo, Is.Not.Null);
            methodInfo.Invoke(_produtosServices, new object[] { produto, produtoDto });
            Assert.That(CompararPropriedades(produto, produtoCopia), Is.False);
        }

        [Test]
        public async Task AlterarAsync_ShouldUpdateProdutoWhenProdutoExists()
        {
            // Arrange
            var produtoId = 1;
            var updateProdutoDto = new UpdateProdutoDTO
            {
                Nome = "Arroz Atualizado",
                Preco = 12,
                QuantEstoqueMin = 2,
                QuantEstoqueMax = 100,
                CategoriaId = 1,
                EstoqueId = 2
            };
            var existingProduto = new Produto
            {
                ProdutoId = produtoId,
                Nome = "Arroz",
                Preco = 10,
                QuantEstoqueMin = 1,
                QuantEstoqueMax = 50,
                Saldo = 30,
                CategoriaId = 1,
                EstoqueId = 2
            };

            var updatedProduto = new Produto
            {
                ProdutoId = produtoId,
                Nome = updateProdutoDto.Nome,
                Preco = updateProdutoDto.Preco,
                QuantEstoqueMin = updateProdutoDto.QuantEstoqueMin,
                QuantEstoqueMax = updateProdutoDto.QuantEstoqueMax,
                Saldo = existingProduto.Saldo,  // Preserve o saldo existente
                CategoriaId = updateProdutoDto.CategoriaId,
                EstoqueId = updateProdutoDto.EstoqueId
            };

            _repositoryProdutos!.Setup(repo => repo.GetByIdAsync(It.IsAny<Expression<Func<Produto, bool>>>())).ReturnsAsync(existingProduto);
            _repositoryProdutos!.Setup(repo => repo.UpdateAsync(It.IsAny<Produto>())).ReturnsAsync(updatedProduto);

            // Act
            var result = await _produtosServices!.AlterarAsync(produtoId, updateProdutoDto);

            // Assert
            Assert.That(result, Is.EqualTo(updatedProduto));
            _repositoryProdutos.Verify(repo => repo.GetByIdAsync(It.IsAny<Expression<Func<Produto, bool>>>()), Times.Once);
            _repositoryProdutos.Verify(repo => repo.UpdateAsync(It.IsAny<Produto>()), Times.Once);

            // Verifique se as propriedades do produto foram atualizadas conforme necessário
            Assert.That(result.Nome, Is.EqualTo(updateProdutoDto.Nome));
            Assert.That(result.Preco, Is.EqualTo(updateProdutoDto.Preco));
            Assert.That(result.QuantEstoqueMin, Is.EqualTo(updateProdutoDto.QuantEstoqueMin));
            Assert.That(result.QuantEstoqueMax, Is.EqualTo(updateProdutoDto.QuantEstoqueMax));
            Assert.That(result.CategoriaId, Is.EqualTo(updateProdutoDto.CategoriaId));
            Assert.That(result.EstoqueId, Is.EqualTo(updateProdutoDto.EstoqueId));
        }

        [Test]
        public void AlterarAsync_ShouldThrowException_WhenProdutoNotFound()
        {
            // Arrange
            var produtoId = 1;
            var updateProdutoDto = new UpdateProdutoDTO
            {
                Nome = "Arroz Atualizado",
                Preco = 12,
                QuantEstoqueMin = 2,
                QuantEstoqueMax = 100,
                CategoriaId = 1,
                EstoqueId = 2
            };

            _repositoryProdutos!.Setup(repo => repo.GetByIdAsync(It.IsAny<Expression<Func<Produto, bool>>>())).ReturnsAsync((Produto?)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _produtosServices!.AlterarAsync(produtoId, updateProdutoDto));
            Assert.That(ex.Message, Is.EqualTo($"Produto com o ID {produtoId} não encontrado."));
            _repositoryProdutos.Verify(repo => repo.GetByIdAsync(It.IsAny<Expression<Func<Produto, bool>>>()), Times.Once);
            _repositoryProdutos.Verify(repo => repo.UpdateAsync(It.IsAny<Produto>()), Times.Never);
        }
    }
}
