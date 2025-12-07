using System;
using System.Threading;
using MediatR;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Mocks
{
    /// <summary>
    /// Mock especializado do IMediator com API fluente.
    /// Totalmente genérico para Commands e Queries.
    /// </summary>
    public class MediatorMockBuilder
    {
        private readonly Mock<IMediator> _mock;

        public MediatorMockBuilder()
        {
            _mock = new Mock<IMediator>();
        }

        public IMediator Object => _mock.Object;

        /// <summary>
        /// Configura retorno usando comparação estrutural (ideal para records).
        /// </summary>
        public MediatorMockBuilder SetupReturn<TRequest, TResponse>(
            TRequest request,
            TResponse response)
            where TRequest : class, IRequest<TResponse>
        {
            _mock.Setup(m => m.Send(
                    It.Is<TRequest>(r => r.Equals(request)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(response!);

            return this;
        }

        /// <summary>
        /// Configura retorno baseado em predicate (caso request tenha parâmetros complexos).
        /// </summary>
        public MediatorMockBuilder SetupReturn<TRequest, TResponse>(
            Func<TRequest, bool> predicate,
            TResponse response)
            where TRequest : class, IRequest<TResponse>
        {
            _mock.Setup(m => m.Send(
                    It.Is<TRequest>(r => predicate(r)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(response!);

            return this;
        }

        /// <summary>
        /// Valida que a chamada ocorreu uma única vez.
        /// </summary>
        public void VerifyCalled<TRequest, TResponse>(TRequest expected)
            where TRequest : class, IRequest<TResponse>
        {
            _mock.Verify(m => m.Send(
                It.Is<TRequest>(r => r.Equals(expected)),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        /// <summary>
        /// Valida que a chamada NÃO ocorreu.
        /// </summary>
        public void VerifyNotCalled<TRequest, TResponse>()
            where TRequest : class, IRequest<TResponse>
        {
            _mock.Verify(m => m.Send(
                    It.IsAny<TRequest>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        public void VerifyCalled<TRequest, TResponse>(Func<TRequest, bool> predicate)
            where TRequest : class, IRequest<TResponse>
        {
            _mock.Verify(m => m.Send(
                It.Is<TRequest>(r => predicate(r)),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

    }
}
