using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Atualizar.Mocks;
using Fiap.FCG.Game.WebApi.Jogos.Atualizar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Atualizar.Fixtures;

public class AtualizarJogoControllerFixture
{
    protected MediatorMockJogos MediatorMock { get; private set; }
    protected AtualizarJogoController Controller { get; private set; }

    protected AtualizarJogoControllerFixture()
    {
        MediatorMock = new MediatorMockJogos();
        Controller = new AtualizarJogoController(MediatorMock.Object);
    }
}