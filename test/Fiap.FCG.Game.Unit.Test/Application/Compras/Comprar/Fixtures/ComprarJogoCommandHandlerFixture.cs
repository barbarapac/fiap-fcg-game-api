using Fiap.FCG.Game.Application.Compras.Comprar;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Tests
{
    public abstract class ComprarJogoCommandHandlerFixture
    {
        protected ComprarJogoCommandHandler Handler { get; }
        protected JogoRepositoryMock JogoRepositoryMock { get; }
        protected PromocaoRepositoryMock PromocaoRepositoryMock { get; }
        protected CompraRepositoryMock CompraRepositoryMock { get; }
        protected BibliotecaRepositoryMock BibliotecaRepositoryMock { get; }
        protected CompraEventPublisherMock CompraEventPublisherMock { get; }

        protected ComprarJogoCommandHandlerFixture()
        {
            JogoRepositoryMock = new();
            PromocaoRepositoryMock = new();
            CompraRepositoryMock = new();
            BibliotecaRepositoryMock = new();
            CompraEventPublisherMock = new();

            Handler = new ComprarJogoCommandHandler(
                JogoRepositoryMock.Object,
                PromocaoRepositoryMock.Object,
                CompraRepositoryMock.Object,
                BibliotecaRepositoryMock.Object,
                CompraEventPublisherMock.Object
            );
        }
    }
}
