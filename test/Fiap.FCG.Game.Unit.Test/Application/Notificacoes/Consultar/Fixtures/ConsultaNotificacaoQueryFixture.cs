using Fiap.FCG.Game.Application.Notificacoes.Consultar;
using Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Consultar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Consultar.Fixtures;

public class ConsultaNotificacaoQueryFixture
{
    protected NotificacaoRepositoryMock NotificacaoRepositoryMock { get; }
    protected ConsultaNotificacaoQuery ConsultaQuery { get; }

    protected ConsultaNotificacaoQueryFixture()
    {
        NotificacaoRepositoryMock = new NotificacaoRepositoryMock();
        ConsultaQuery = new ConsultaNotificacaoQuery(NotificacaoRepositoryMock.Object);
    }
}