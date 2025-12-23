using Fiap.FCG.Game.Application.Eventos.ComprasEvent;
using Moq;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks
{
    public class CompraEventPublisherMock : Mock<ICompraEventPublisher>
    {
        public void ConfigurarPublicarCompraRealizadaAsync()
        {
            Setup(p => p.PublicarCompraRealizadaAsync(It.IsAny<CompraRealizadaEvent>()))
                .Returns(Task.CompletedTask);
        }

        public void GarantirPublicarCompraRealizadaAsync(int compraId, int usuarioId, decimal valorTotal)
        {
            Verify(p => p.PublicarCompraRealizadaAsync(
                It.Is<CompraRealizadaEvent>(e =>
                    e.CompraId == compraId &&
                    e.UsuarioId == usuarioId &&
                    e.ValorTotal == valorTotal
                )
            ), Times.Once);
        }
    }
}
