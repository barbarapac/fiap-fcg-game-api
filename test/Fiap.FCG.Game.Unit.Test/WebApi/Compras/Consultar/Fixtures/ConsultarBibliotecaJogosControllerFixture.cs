using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Mocks;
using Fiap.FCG.Game.WebApi.Compras.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fixtures
{
    public class ConsultarBibliotecaJogosControllerFixture
    {
        protected readonly MediatorMockBuilder MediatorMock;
        protected readonly ConsultarBibliotecaJogosController Controller;

        public ConsultarBibliotecaJogosControllerFixture()
        {
            MediatorMock = new MediatorMockBuilder();
            Controller = new ConsultarBibliotecaJogosController(MediatorMock.Object);
        }
    }
}