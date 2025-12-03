using System.Threading.Tasks;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Infrastructure.PublisherEvent.GameEvent;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Atualizar.Mocks;

public class GameEventPublisherMock : Mock<IGameEventPublisher>
{
    public void ConfigurarJogoEditadoPublishAsync()
    {
        Setup(publisher => publisher.JogoEditadoPublishAsync(It.IsAny<Jogo>()))
            .Returns(Task.CompletedTask);
    }

    public void GarantirJogoEditadoPublishAsyncChamado(Jogo jogo)
    {
        Verify(publisher => publisher.JogoEditadoPublishAsync(jogo), Times.Once);
    }

    public void GarantirJogoEditadoPublishAsyncNaoChamado()
    {
        Verify(publisher => publisher.JogoEditadoPublishAsync(It.IsAny<Jogo>()), Times.Never);
    }
}