using System.Collections.Generic;
using System.Threading;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Mocks;

public class UsuarioNotificationGatewayMock : Mock<IUsuarioNotificationGateway>
{
    public void ConfigurarUsuariosNotificaveis(List<UsuarioNotificavelDto> usuarios)
    {
        Setup(x => x.ObterUsuariosNotificaveisHttpAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(usuarios);
    }
}