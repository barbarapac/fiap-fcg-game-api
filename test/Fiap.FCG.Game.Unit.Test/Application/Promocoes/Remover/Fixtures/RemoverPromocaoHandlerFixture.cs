using Fiap.FCG.Game.Application.Promocoes.Remover;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover.Fixtures;

public class RemoverPromocaoHandlerFixture
{
    protected PromocaoRepositoryMock PromocaoRepositoryMock { get; }
    protected RemoverPromocaoHandler Handler { get; }

    public RemoverPromocaoHandlerFixture()
    {
        PromocaoRepositoryMock = new PromocaoRepositoryMock();
        Handler = new RemoverPromocaoHandler(PromocaoRepositoryMock.Object);
    }
}