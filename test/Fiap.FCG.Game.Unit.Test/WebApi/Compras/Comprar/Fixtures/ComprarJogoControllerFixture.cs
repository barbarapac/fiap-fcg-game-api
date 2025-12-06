using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Comprar.Mocks;
using Fiap.FCG.Game.WebApi.Compras.Comprar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Comprar.Fixtures
{
    public class ComprarJogoControllerFixture
    {
        protected readonly MediatorMockBuilder MediatorMock;
        protected readonly ComprarJogoController Controller;

        public ComprarJogoControllerFixture()
        {
            MediatorMock = new MediatorMockBuilder();
            Controller = new ComprarJogoController(MediatorMock.Object);
        }
    }
}