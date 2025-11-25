using System;
using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Notificacoes.Enviar;
using MediatR;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Mocks;

public class MediatorMock : Mock<IMediator>
{
    public void ConfigurarEnvioComSucesso()
    {
        Setup(x => x.Send(It.IsAny<EnviarNotificacaoCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
    }

    public void ConfigurarErroNoEnvio()
    {
        Setup(x => x.Send(It.IsAny<EnviarNotificacaoCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro simulado"));
    }

    public void GarantirEnvioFoiChamado()
    {
        Verify(x => x.Send(It.IsAny<EnviarNotificacaoCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }
}