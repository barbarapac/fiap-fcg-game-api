using Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Consultar.Mocks;
using Fiap.FCG.Game.WebApi.Notificacoes.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Consultar.Fixtures;

public abstract class ConsultarNotificacoesControllerFixture
{
    protected ConsultaNotificacaoQueryMock QueryMock { get; private set; }
    protected ConsultarNotificacoesController Controller { get; private set; }

    protected ConsultarNotificacoesControllerFixture()
    {
        QueryMock = new ConsultaNotificacaoQueryMock();
        Controller = new ConsultarNotificacoesController(QueryMock.Object);
    }
}