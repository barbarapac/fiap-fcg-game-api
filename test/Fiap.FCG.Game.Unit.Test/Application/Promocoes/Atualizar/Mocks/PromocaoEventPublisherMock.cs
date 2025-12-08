using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Eventos.PromocaoEvent;
using Fiap.FCG.Game.Domain.Promocoes;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Mocks;

public class PromocaoEventPublisherMock : Mock<IPromocaoEventPublisher>
{
    public void ConfigurarPromocaoEditadaPublishAsync()
    {
        Setup(p => p.PromocaEditadaPublishAsync(It.IsAny<Promocao>()))
            .Returns(Task.CompletedTask);
    }

    public void GarantirPromocaoEditadaPublishAsyncChamado(Promocao promocao)
    {
        Verify(p => p.PromocaEditadaPublishAsync(promocao), Times.Once);
    }

    public void GarantirPromocaoEditadaPublishAsyncNaoChamado()
    {
        Verify(p => p.PromocaEditadaPublishAsync(It.IsAny<Promocao>()), Times.Never);
    }
}