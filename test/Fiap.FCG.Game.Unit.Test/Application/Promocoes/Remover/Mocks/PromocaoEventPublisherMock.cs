using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Eventos.PromocaoEvent;
using Fiap.FCG.Game.Domain.Promocoes;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover.Mocks;

public class PromocaoEventPublisherMock : Mock<IPromocaoEventPublisher>
{
    public void ConfigurarPromocaoRemovidaPublishAsync()
    {
        Setup(p => p.PromocaoRemovidaPublishAsync(It.IsAny<Promocao>()))
            .Returns(Task.CompletedTask);
    }

    public void GarantirPromocaoRemovidaPublishAsyncChamado(Promocao promocao)
    {
        Verify(p => p.PromocaoRemovidaPublishAsync(promocao), Times.Once);
    }

    public void GarantirPromocaoRemovidaPublishAsyncNaoChamado()
    {
        Verify(p => p.PromocaoRemovidaPublishAsync(It.IsAny<Promocao>()), Times.Never);
    }
}