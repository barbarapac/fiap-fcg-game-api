using System;
using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.User.WebApi.Protos;
using Grpc.Core;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Mocks
{
    public class UsuarioServiceClientMock : Mock<UsuarioService.UsuarioServiceClient>
    {
        public void ConfigurarResposta(ObterUsuariosComNotificacoesResponse resposta)
        {
            var fakeCall = CreateAsyncUnaryCall(resposta);

            Setup(x => x.ObterUsuariosComNotificacoesAsync(
                It.IsAny<ObterUsuariosComNotificacoesRequest>(),
                It.IsAny<Metadata>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>())
            ).Returns(fakeCall);
        }

        public void GarantirMetodoChamado()
        {
            Verify(x => x.ObterUsuariosComNotificacoesAsync(
                    It.IsAny<ObterUsuariosComNotificacoesRequest>(),
                    It.IsAny<Metadata>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private AsyncUnaryCall<T> CreateAsyncUnaryCall<T>(T response)
        {
            return new AsyncUnaryCall<T>(
                Task.FromResult(response),
                Task.FromResult(new Metadata()),
                () => Status.DefaultSuccess,
                () => new Metadata(),
                () => { }
            );
        }
    }
}