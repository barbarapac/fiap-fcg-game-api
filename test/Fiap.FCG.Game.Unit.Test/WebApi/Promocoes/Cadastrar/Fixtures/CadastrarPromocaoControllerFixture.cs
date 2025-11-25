using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Cadastrar.Mocks;
using Fiap.FCG.Game.WebApi.Promocoes.Cadastrar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Cadastrar.Fixtures;

public class CadastrarPromocaoControllerFixture
{
    protected MediatorMock MediatorMock { get; private set; }
    protected CadastrarPromocaoController Controller { get; private set; }

    public CadastrarPromocaoControllerFixture()
    {
        MediatorMock = new MediatorMock();
        Controller = new CadastrarPromocaoController(MediatorMock.Object);
    }
}