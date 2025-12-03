using Fiap.FCG.Game.Application.Jogos.Atualizar;
using Fiap.FCG.Game.Unit.Test.Application.Jogos.Atualizar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Atualizar.Fixtures;

public abstract class AtualizarJogoHandlerFixture
{
    protected JogoRepositoryMock JogoRepositoryMock { get; }
    protected AtualizarJogoHandler Handler { get; }
    protected GameEventPublisherMock GameEventPublisherMock { get; }

    protected AtualizarJogoHandlerFixture()
    {
        JogoRepositoryMock = new JogoRepositoryMock();
        GameEventPublisherMock = new GameEventPublisherMock();
        Handler = new AtualizarJogoHandler(JogoRepositoryMock.Object, GameEventPublisherMock.Object);
    }
}