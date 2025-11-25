using System;
using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Fixtures;
using Fiap.FCG.Game.WebApi.Notificacoes.Enviar;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar;

public class EnviarNotificacoesJobTest : EnviarNotificacoesJobFixture
{
    [Fact]
    public void ObterIntervalo_QuandoConfiguracaoValida_DeveRetornarTimeSpanCorreto()
    {
        // Arrange
        ConfigurationMock.ConfigurarIntervalo(ConfigurationFaker.IntervaloValidoEmMinutos());

        // Act
        var intervalo = typeof(EnviarNotificacoesJob)
            .GetMethod("ObterIntervalo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(Job, null);

        // Assert
        intervalo.Should().Be(TimeSpan.FromMinutes(1));
    }

    [Fact]
    public void ObterIntervalo_QuandoConfiguracaoInvalida_DeveRetornarPadrao()
    {
        // Arrange
        ConfigurationMock.ConfigurarIntervalo(ConfigurationFaker.IntervaloInvalido());

        // Act
        var intervalo = typeof(EnviarNotificacoesJob)
            .GetMethod("ObterIntervalo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(Job, null);

        // Assert
        intervalo.Should().Be(TimeSpan.FromMinutes(60));
    }

    [Fact]
    public async Task ExecuteAsync_QuandoExecutadoComSucesso_DeveChamarMediatorELogar()
    {
        // Arrange
        ConfigurationMock.ConfigurarIntervalo("1");
        MediatorMock.ConfigurarEnvioComSucesso();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));

        // Act
        await Job.StartAsync(cts.Token);
        await Task.Delay(1500, cts.Token);
        await Job.StopAsync(cts.Token);

        // Assert
        MediatorMock.GarantirEnvioFoiChamado();
        LoggerMock.GarantirLogDeSucesso();
    }

    [Fact]
    public async Task ExecuteAsync_QuandoOcorrerExcecao_DeveLogarErro()
    {
        // Arrange
        ConfigurationMock.ConfigurarIntervalo("1");
        MediatorMock.ConfigurarErroNoEnvio();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));

        // Act
        await Job.StartAsync(cts.Token);
        await Task.Delay(1500, cts.Token);
        await Job.StopAsync(cts.Token);

        // Assert
        LoggerMock.GarantirLogDeErro();
    }
}
