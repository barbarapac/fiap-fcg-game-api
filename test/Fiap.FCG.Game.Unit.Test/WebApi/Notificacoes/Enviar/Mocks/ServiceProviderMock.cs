using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Mocks;

public class ServiceProviderMock : Mock<IServiceProvider>
{
    public void ConfigurarMediator(IMediator mediator)
    {
        var scopeMock = new Mock<IServiceScope>();
        var serviceProviderMock = new Mock<IServiceProvider>();

        serviceProviderMock.Setup(x => x.GetService(typeof(IMediator)))
            .Returns(mediator);

        scopeMock.Setup(x => x.ServiceProvider)
            .Returns(serviceProviderMock.Object);

        var scopeFactory = new Mock<IServiceScopeFactory>();
        scopeFactory.Setup(x => x.CreateScope())
            .Returns(scopeMock.Object);

        Setup(x => x.CreateScope())
            .Returns(scopeMock.Object);
    }
}