using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Eventos.GameEvent;
using Fiap.FCG.Game.Domain.Jogos;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Cadastrar.Mocks;

public class GameEventPublisherMock : Mock<IGameEventPublisher>
{
    public void ConfigurarJogoCadastradoPublishAsync()
    {
        Setup(publisher => publisher.JogoCadastradoPublishAsync(It.IsAny<Jogo>()))
            .Returns(Task.CompletedTask);
    }

    public void GarantirJogoCadastradoPublishAsyncChamado(Jogo jogo)
    {
        Verify(publisher => publisher.JogoCadastradoPublishAsync(jogo), Times.Once);
    }

    public void GarantirJogoCadastradoPublishAsyncNaoChamado()
    {
        Verify(publisher => publisher.JogoCadastradoPublishAsync(It.IsAny<Jogo>()), Times.Never);
    }
}