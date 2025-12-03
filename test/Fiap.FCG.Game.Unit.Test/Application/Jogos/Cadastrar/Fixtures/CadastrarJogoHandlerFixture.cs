using Fiap.FCG.Game.Application.Jogos.Cadastrar;
using Fiap.FCG.Game.Unit.Test.Application.Jogos.Cadastrar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Cadastrar.Fixtures
{
    public class CadastrarJogoHandlerFixture
    {
        protected JogoRepositoryMock JogoRepositoryMock { get; private set; }
        protected GameEventPublisherMock GameEventPublisherMock { get; private set; }
        protected CadastrarJogoHandler Handler { get; private set; }

        public CadastrarJogoHandlerFixture()
        {
            JogoRepositoryMock = new JogoRepositoryMock();
            GameEventPublisherMock = new GameEventPublisherMock();

            Handler = new CadastrarJogoHandler(
                JogoRepositoryMock.Object,
                GameEventPublisherMock.Object
            );
        }
    }
}