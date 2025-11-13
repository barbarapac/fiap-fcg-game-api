using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Cadastrar.Mocks;
using Fiap.FCG.Game.WebApi.Jogos.Cadastrar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Cadastrar.Fixtures;

public class CadastrarJogoControllerFixture
{
    protected MediatorMockJogos MediatorMock { get; private set; }
    protected CadastrarJogoController Controller { get; private set; }

    public CadastrarJogoControllerFixture()
    {
        MediatorMock = new MediatorMockJogos();
        Controller = new CadastrarJogoController(MediatorMock.Object);
    }
}
