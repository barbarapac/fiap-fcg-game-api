using Fiap.FCG.Game.Infrastructure.Usuarios;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Fixtures;

public abstract class UsuarioNotificationGatewayFixture
{
    protected UsuarioServiceClientMock UsuarioServiceMock { get; private set; }

    protected UsuarioNotificationGateway Gateway { get; private set; }

    protected UsuarioNotificationGatewayFixture()
    {
        UsuarioServiceMock = new UsuarioServiceClientMock();

        Gateway = new UsuarioNotificationGateway(
            UsuarioServiceMock.Object
        );
    }
}