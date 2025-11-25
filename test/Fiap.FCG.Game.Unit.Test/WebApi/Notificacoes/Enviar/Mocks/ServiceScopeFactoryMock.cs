using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Mocks;

public class ServiceScopeFactoryMock : Mock<IServiceScopeFactory>
{
    public void ConfigurarScopeCom(IMediator mediator)
    {
        var scopeMock = new Mock<IServiceScope>();
        var serviceProviderMock = new Mock<IServiceProvider>();

        serviceProviderMock
            .Setup(x => x.GetService(typeof(IMediator)))
            .Returns(mediator);

        scopeMock.Setup(x => x.ServiceProvider).Returns(serviceProviderMock.Object);

        Setup(x => x.CreateScope()).Returns(scopeMock.Object);
    }
}