using Fiap.FCG.Game.Application.Notificacoes.Enviar;
using Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Fixtures;

public abstract class EnviarNotificacaoHandlerFixture
{
    protected PromocaoRepositoryMock PromocaoRepositoryMock { get; private set; }
    protected NotificacaoRepositoryMock NotificacaoRepositoryMock { get; private set; }
    protected EmailSenderMock EmailSenderMock { get; private set; }
    protected UsuarioNotificationGatewayMock UsuarioGatewayMock { get; private set; }

    protected EnviarNotificacaoHandler Handler { get; private set; }

    protected EnviarNotificacaoHandlerFixture()
    {
        PromocaoRepositoryMock = new PromocaoRepositoryMock();
        NotificacaoRepositoryMock = new NotificacaoRepositoryMock();
        EmailSenderMock = new EmailSenderMock();
        UsuarioGatewayMock = new UsuarioNotificationGatewayMock();

        Handler = new EnviarNotificacaoHandler(
            PromocaoRepositoryMock.Object,
            NotificacaoRepositoryMock.Object,
            EmailSenderMock.Object,
            UsuarioGatewayMock.Object
        );
    }
}