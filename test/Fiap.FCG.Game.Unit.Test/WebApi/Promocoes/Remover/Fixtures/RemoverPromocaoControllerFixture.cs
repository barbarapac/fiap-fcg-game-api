using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Remover.Mocks;
using Fiap.FCG.Game.WebApi.Promocoes.Remover;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Remover.Fixtures
{
    public class RemoverPromocaoControllerFixture
    {
        protected MediatorMock MediatorMock { get; }
        protected RemoverPromocaoController Controller { get; }

        public RemoverPromocaoControllerFixture()
        {
            MediatorMock = new MediatorMock();
            Controller = new RemoverPromocaoController(MediatorMock.Object);
        }
    }
}