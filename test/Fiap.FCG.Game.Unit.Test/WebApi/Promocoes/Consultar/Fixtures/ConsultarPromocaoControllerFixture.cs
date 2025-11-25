using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Consultar.Mocks;
using Fiap.FCG.Game.WebApi.Promocoes.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Consultar.Fixtures;

public abstract class ConsultarPromocaoControllerFixture
{
    protected ConsultarPromocaoController Controller { get; }
    protected ConsultaPromocaoQueryMock ConsultaPromocaoQueryMock { get; }

    protected ConsultarPromocaoControllerFixture()
    {
        ConsultaPromocaoQueryMock = new ConsultaPromocaoQueryMock();
        Controller = new ConsultarPromocaoController(ConsultaPromocaoQueryMock.Object);
    }
}