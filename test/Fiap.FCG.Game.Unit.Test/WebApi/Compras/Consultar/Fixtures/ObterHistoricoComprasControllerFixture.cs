using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Mocks;
using Fiap.FCG.Game.WebApi.Compras.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fixtures
{
    public class ObterHistoricoComprasControllerFixture
    {
        protected readonly MediatorMockBuilder MediatorMock;
        protected readonly ConsultarHistoricoComprasController Controller;

        public ObterHistoricoComprasControllerFixture()
        {
            MediatorMock = new MediatorMockBuilder();
            Controller = new ConsultarHistoricoComprasController(MediatorMock.Object);
        }
    }
}