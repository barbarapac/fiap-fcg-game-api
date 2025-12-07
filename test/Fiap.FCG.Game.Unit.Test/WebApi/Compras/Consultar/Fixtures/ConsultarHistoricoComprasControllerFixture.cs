using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Mocks;
using Fiap.FCG.Game.WebApi.Compras.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fixtures
{
    public abstract class ConsultarHistoricoComprasControllerFixture
    {
        protected MediatorMockBuilder MediatorBuilder { get; }
        protected ConsultarHistoricoComprasController Controller { get; }

        protected ConsultarHistoricoComprasControllerFixture()
        {
            MediatorBuilder = new MediatorMockBuilder();
            Controller = new ConsultarHistoricoComprasController(MediatorBuilder.Object);
        }
    }
}
