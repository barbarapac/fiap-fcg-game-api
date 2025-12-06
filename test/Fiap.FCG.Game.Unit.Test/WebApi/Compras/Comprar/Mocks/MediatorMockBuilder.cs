using System.Threading;
using Fiap.FCG.Game.Application.Compras.Comprar;
using Fiap.FCG.Game.Domain._Shared;
using MediatR;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Comprar.Mocks
{
    public class MediatorMockBuilder
    {
        private readonly Mock<IMediator> _mock;

        public MediatorMockBuilder() => _mock = new Mock<IMediator>();

        public IMediator Object => _mock.Object;

        public void SetupReturn(ComprarJogoCommand command, Result<bool> result)
        {
            _mock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
        }

        public void VerifyCalled(ComprarJogoCommand command)
        {
            _mock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}