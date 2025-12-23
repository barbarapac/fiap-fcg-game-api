using Fiap.FCG.Game.Domain.Compras;
using Moq;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks
{
    public class CompraRepositoryMock : Mock<ICompraRepository>
    {
        public void ConfigurarAdicionarAsyncComId(int idSimulado)
        {
            Setup(x => x.AdicionarAsync(It.IsAny<HistoricoCompra>()))
            .Callback<HistoricoCompra>(compra =>
            {                
                typeof(HistoricoCompra)
                .GetProperty("Id")!
                .SetValue(compra, idSimulado);
            })
            .Returns(Task.CompletedTask);
        }


        public void GarantirAdicionarAsync() =>
        Verify(x => x.AdicionarAsync(It.IsAny<HistoricoCompra>()), Times.Once);
    }
}
