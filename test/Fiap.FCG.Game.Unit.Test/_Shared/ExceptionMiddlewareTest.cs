using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test._Shared.Fixtures;
using Fiap.FCG.Game.WebApi._Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test._Shared;

public class ExceptionMiddlewareTest : ExceptionMiddlewareFixture
{
    [Fact]
    public async Task InvokeAsync_EmAmbienteDesenvolvimento_DeveRetornarErroComStackTrace()
    {
        // Arrange
        HostEnvironmentMock.ConfigurarComoDesenvolvimento();
        var middleware = CriarMiddlewareQueLancaExcecao();

        var context = new DefaultHttpContext();
        context.TraceIdentifier = "TRACE-123";
        context.Response.Body = new MemoryStream();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        context.Response.ContentType.Should().Be("application/json");

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body);
        var body = await reader.ReadToEndAsync();

        var erro = JsonSerializer.Deserialize<ErrorDetails>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        erro!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        erro.Message.Should().Be("Erro de teste");
        erro.Trace.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task InvokeAsync_EmAmbienteProducao_DeveRetornarErroSemStackTrace()
    {
        // Arrange
        HostEnvironmentMock.ConfigurarComoProducao();
        var middleware = CriarMiddlewareQueLancaExcecao();

        var context = new DefaultHttpContext();
        context.TraceIdentifier = "TRACE-456";
        context.Response.Body = new MemoryStream();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        context.Response.ContentType.Should().Be("application/json");

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body);
        var body = await reader.ReadToEndAsync();

        var erro = JsonSerializer.Deserialize<ErrorDetails>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        erro!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        erro.Message.Should().Be("Erro de teste");
        erro.Trace.Should().BeNull();
    }
}
