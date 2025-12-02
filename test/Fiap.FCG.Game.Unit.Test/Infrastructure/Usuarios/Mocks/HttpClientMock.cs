using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Mocks;

public class HttpClientMock
{
    private readonly Mock<HttpMessageHandler> _handlerMock;

    public HttpClientMock()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
    }

    public HttpClient HttpClient => new HttpClient(_handlerMock.Object);

    public void ConfigurarGetComRetorno<T>(T retorno)
    {
        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(retorno)
            });
    }

    public void ConfigurarGetComErro()
    {
        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
    }

    public void GarantirRequisicaoGetFoiFeita()
    {
        _handlerMock
            .Protected()
            .Verify("SendAsync", Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString().Contains("/api/usuarios/com-notificacoes")),
                ItExpr.IsAny<CancellationToken>());
    }
}