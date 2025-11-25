using System.Collections.Generic;
using Fiap.FCG.Game.Domain.Notificacoes;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Mocks;

public class NotificacaoRepositoryMock : Mock<INotificacaoRepository>
{
    public void ConfigurarUsuariosNaoNotificados(List<int> resultado)
    {
        Setup(x => x.ObterUsuariosNaoNotificadosAsync(It.IsAny<int>(), It.IsAny<List<int>>()))
            .ReturnsAsync(resultado);
    }

    public void GarantirSaveChangesChamado()
    {
        Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    public void GarantirAdicionarFoiChamado()
    {
        Verify(x => x.Adicionar(It.IsAny<Notificacao>()), Times.AtLeastOnce);
    }
}