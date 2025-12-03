using Fiap.FCG.Game.Application.Promocoes.Atualizar;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Fixtures;

public class AtualizarPromocaoHandlerFixture
{
    protected JogoRepositoryMock JogoRepositoryMock { get; private set; }
    protected PromocaoRepositoryMock PromocaoRepositoryMock { get; private set; }
    protected PromocaoEventPublisherMock PromocaoEventPublisherMock { get; private set; }
    protected AtualizarPromocaoHandler Handler { get; private set; }

    protected AtualizarPromocaoHandlerFixture()
    {
        JogoRepositoryMock            = new JogoRepositoryMock();
        PromocaoRepositoryMock        = new PromocaoRepositoryMock();
        PromocaoEventPublisherMock    = new PromocaoEventPublisherMock();

        Handler = new AtualizarPromocaoHandler(
            JogoRepositoryMock.Object,
            PromocaoRepositoryMock.Object,
            PromocaoEventPublisherMock.Object
        );
    }
}