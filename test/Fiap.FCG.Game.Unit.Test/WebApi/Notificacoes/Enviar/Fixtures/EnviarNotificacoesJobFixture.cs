using System;
using Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Mocks;
using Fiap.FCG.Game.WebApi.Notificacoes.Enviar;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Fixtures;

public abstract class EnviarNotificacoesJobFixture
{
    protected MediatorMock MediatorMock { get; private set; }
    protected LoggerMock LoggerMock { get; private set; }
    protected ConfigurationMock ConfigurationMock { get; private set; }
    protected ServiceScopeFactoryMock ScopeFactoryMock { get; private set; }
    protected IServiceProvider ServiceProvider { get; private set; }

    protected EnviarNotificacoesJob Job { get; private set; }

    protected EnviarNotificacoesJobFixture()
    {
        MediatorMock = new MediatorMock();
        LoggerMock = new LoggerMock();
        ConfigurationMock = new ConfigurationMock();
        ScopeFactoryMock = new ServiceScopeFactoryMock();

        ScopeFactoryMock.ConfigurarScopeCom(MediatorMock.Object);

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock
            .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
            .Returns(ScopeFactoryMock.Object);

        ServiceProvider = serviceProviderMock.Object;

        Job = new EnviarNotificacoesJob(
            ServiceProvider,
            LoggerMock.Object,
            ConfigurationMock.Object
        );
    }
}