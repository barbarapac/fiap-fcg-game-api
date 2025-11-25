using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Mocks;

public class LoggerMock : Mock<ILogger<Fiap.FCG.Game.WebApi.Notificacoes.Enviar.EnviarNotificacoesJob>>
{
    public void GarantirLogDeSucesso()
    {
        VerifyLog(LogLevel.Information, "Notificações enviadas com sucesso.");
    }

    public void GarantirLogDeErro()
    {
        VerifyLog(LogLevel.Error);
    }

    private void VerifyLog(LogLevel level, string? mensagemContendo = null)
    {
        Verify(x => x.Log(
                level,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    mensagemContendo == null || v.ToString().Contains(mensagemContendo)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.AtLeastOnce);
    }
}