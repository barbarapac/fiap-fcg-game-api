using Fiap.FCG.Game.Application.Promocoes.Remover;
using PromocaoEventPublisherMock = Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover.Mocks.PromocaoEventPublisherMock;
using PromocaoRepositoryMock = Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover.Mocks.PromocaoRepositoryMock;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover.Fixtures;

public class RemoverPromocaoHandlerFixture
{
    protected PromocaoRepositoryMock PromocaoRepositoryMock { get; }
    protected PromocaoEventPublisherMock PromocaoEventPublisherMock { get; }
    protected RemoverPromocaoHandler Handler { get; }

    public RemoverPromocaoHandlerFixture()
    {
        PromocaoRepositoryMock = new PromocaoRepositoryMock();
        PromocaoEventPublisherMock = new PromocaoEventPublisherMock();

        Handler = new RemoverPromocaoHandler(
            PromocaoRepositoryMock.Object,
            PromocaoEventPublisherMock.Object
        );
    }
}