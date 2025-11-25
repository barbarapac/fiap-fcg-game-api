using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Atualizar.Mocks;
using Fiap.FCG.Game.WebApi.Promocoes.Atualizar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Atualizar.Fixtures;

public class AtualizarPromocaoControllerFixture
{
    protected MediatorMock MediatorMock { get; private set; }
    protected AtualizarPromocaoController Controller { get; private set; }

    protected AtualizarPromocaoControllerFixture()
    {
        MediatorMock = new MediatorMock();
        Controller   = new AtualizarPromocaoController(MediatorMock.Object);
    }
}