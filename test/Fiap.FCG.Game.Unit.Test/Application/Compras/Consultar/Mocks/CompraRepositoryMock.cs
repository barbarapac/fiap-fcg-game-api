using Fiap.FCG.Game.Domain.Compras;
using Moq;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Mocks
{
    public class CompraRepositoryMock : Mock<ICompraRepository>
    {
        public void ConfigurarObterPorUsuario(List<HistoricoCompra> resultado)
        {
            Setup(x => x.ObterPorUsuarioAsync(It.IsAny<int>()))
                .ReturnsAsync(resultado);
        }

        public void GarantirObterPorUsuarioChamadoCom(int usuarioId)
        {
            Verify(x => x.ObterPorUsuarioAsync(usuarioId), Times.Once);
        }
    }
}
