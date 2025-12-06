using Fiap.FCG.Game.Application.Compras.Consultar;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fixtures
{
    public abstract class ConsultarBibliotecaHandlerFixture
    {
        protected ConsultarBibliotecaHandler Handler { get; }
        protected BibliotecaRepositoryMock BibliotecaRepositoryMock { get; }


        protected ConsultarBibliotecaHandlerFixture()
        {
            BibliotecaRepositoryMock = new();


            Handler = new ConsultarBibliotecaHandler(BibliotecaRepositoryMock.Object);
        }
    }
}
