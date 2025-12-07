using Fiap.FCG.Game.Application.Compras.Consultar;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fixtures
{
    public abstract class ConsultarHistoricoHandlerFixture
    {
        protected CompraRepositoryMock RepositoryMock { get; }
        protected ConsultarHistoricoHandler Handler { get; }

        protected ConsultarHistoricoHandlerFixture()
        {
            RepositoryMock = new CompraRepositoryMock();
            Handler = new ConsultarHistoricoHandler(RepositoryMock.Object);
        }
    }
}
