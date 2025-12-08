using Fiap.FCG.Game.Domain.Compras;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks
{
    public class CompraRepositoryMock : Mock<ICompraRepository>
    {
        public void GarantirAdicionarAsync() =>
        Verify(x => x.AdicionarAsync(It.IsAny<HistoricoCompra>()), Times.Once);
    }
}
