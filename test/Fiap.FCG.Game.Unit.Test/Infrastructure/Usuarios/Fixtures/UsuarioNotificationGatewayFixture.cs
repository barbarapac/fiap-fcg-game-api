using System.Collections.Generic;
using Fiap.FCG.Game.Infrastructure.Usuarios;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Mocks;
using Microsoft.Extensions.Configuration;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Fixtures;

public abstract class UsuarioNotificationGatewayFixture
{
    protected UsuarioServiceClientMock UsuarioServiceMock { get; private set; }
    protected HttpClientMock HttpClientMock { get; private set; }
    protected UsuarioNotificationGateway Gateway { get; private set; }

    protected UsuarioNotificationGatewayFixture()
    {
        UsuarioServiceMock = new UsuarioServiceClientMock();
        HttpClientMock = new HttpClientMock();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                ["URI_USUARIO_API"] = "http://localhost"
            }).Build();

        Gateway = new UsuarioNotificationGateway(
            UsuarioServiceMock.Object,
            HttpClientMock.HttpClient,
            configuration
        );
    }
}