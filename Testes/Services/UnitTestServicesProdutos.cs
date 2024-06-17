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
        
    }
}
