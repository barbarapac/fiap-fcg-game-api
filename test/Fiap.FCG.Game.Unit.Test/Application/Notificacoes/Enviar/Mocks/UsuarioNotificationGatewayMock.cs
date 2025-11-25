using System.Collections.Generic;
using System.Threading;
using Fiap.FCG.Game.Infrastructure.Usuarios;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Mocks;

public class UsuarioNotificationGatewayMock : Mock<IUsuarioNotificationGateway>
{
    public void ConfigurarUsuariosNotificaveis(List<UsuarioNotificavelDto> usuarios)
    {
        Setup(x => x.ObterUsuariosNotificaveisAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(usuarios);
    }
}