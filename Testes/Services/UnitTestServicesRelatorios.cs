using System.Linq.Expressions;
using EstocariaNet.Models;
using EstocariaNet.Services;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.Repositories.Interfaces;
using Moq;

namespace Testes.Services
{
    [TestFixture]
    public class UnitTestServicesRelatorios
    {
        private Mock<ILancamentosServices>? _lancamentosServices;
        private Mock<IRepositoryRelatorios>? _repositoryRelatorios;
        private RelatoriosServices? _relatoriosServices;

        [SetUp]
        public void SetUp()
        {
            _repositoryRelatorios = new Mock<IRepositoryRelatorios>();
            _lancamentosServices = new Mock<ILancamentosServices>();

            _relatoriosServices = new RelatoriosServices(_repositoryRelatorios.Object, _lancamentosServices.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _repositoryRelatorios = null;
            _lancamentosServices = null;
            _relatoriosServices = null;
        }

        [Test]
        public async Task TestRelatorios()
        {
            var dataInicial = new DateTime(2023, 1, 1);
            var dataFinal = new DateTime(2023, 12, 31);

            var expectedRelatorios = new List<Relatorio>{
                new Relatorio { RelatorioId = 1, Data = new DateTime(2023, 6, 15) },
                new Relatorio { RelatorioId = 2, Data = new DateTime(2023, 9, 10) },};

            _repositoryRelatorios!.Setup(r => r.GetRelatoriosInitDataToEndDataAsync(It.IsAny<Expression<Func<Relatorio, bool>>>()))
            .ReturnsAsync((Expression<Func<Relatorio, bool>> predicate) => expectedRelatorios.AsQueryable().Where(predicate).ToList());

            // Act
            var result = await _relatoriosServices!.BuscarTodosInitDataToEndDataAsync(dataInicial, dataFinal);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(),Is.EqualTo(2));
        }

    }
}
