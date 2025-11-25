using System.Collections.Generic;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Consultar.Mocks;

public class ConsultaNotificacaoQueryMock : Mock<IConsultaNotificacaoQuery>
{
    public void ConfigurarObterTodasAsync(List<NotificacaoResponse> resultado)
    {
        Setup(x => x.ObterTodasAsync())
            .ReturnsAsync(resultado);
    }

    public void ConfigurarObterPorIdUsuarioAsync(int usuarioId, List<NotificacaoResponse> resultado)
    {
        Setup(x => x.ObterPorIdUsuarioAsync(usuarioId))
            .ReturnsAsync(resultado);
    }

    public void GarantirChamadaDeObterPorIdUsuarioAsync(int usuarioId)
    {
        Verify(x => x.ObterPorIdUsuarioAsync(usuarioId), Times.Once);
    }

    public void GarantirChamadaDeObterTodasAsync()
    {
        Verify(x => x.ObterTodasAsync(), Times.Once);
    }
}