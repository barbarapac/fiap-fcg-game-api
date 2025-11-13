using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Mocks;
using Fiap.FCG.Game.WebApi.Jogos.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Fixtures;

public class ConsultarJogoControllerFixture
{
    protected ConsultaJogoQueryMock ConsultaMock { get; private set; }
    protected ConsultarJogoController Controller { get; private set; }

    protected ConsultarJogoControllerFixture()
    {
        ConsultaMock = new ConsultaJogoQueryMock();
        Controller = new ConsultarJogoController(ConsultaMock.Object);
    }
}