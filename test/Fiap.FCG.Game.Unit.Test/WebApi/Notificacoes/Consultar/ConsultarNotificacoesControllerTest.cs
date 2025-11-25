using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Consultar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Consultar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Consultar;

public class ConsultarNotificacoesControllerTest : ConsultarNotificacoesControllerFixture
{
    [Fact]
    public async Task ListarTodas_Sempre_DeveRetornarStatusOkComLista()
    {
        // Arrange
        var notificacoes = NotificacaoFaker.ListaValida();
        QueryMock.ConfigurarObterTodasAsync(notificacoes);

        // Act
        var resultado = await Controller.ListarTodas();

        // Assert
        var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(notificacoes);
        QueryMock.GarantirChamadaDeObterTodasAsync();
    }

    [Fact]
    public async Task ObterPorIdUsuario_QuandoNaoExistemNotificacoes_DeveRetornarNotFound()
    {
        // Arrange
        var usuarioId = 123;
        QueryMock.ConfigurarObterPorIdUsuarioAsync(usuarioId, NotificacaoFaker.ListaVazia());

        // Act
        var resultado = await Controller.ObterPorIdUsuario(usuarioId);

        // Assert
        var notFound = resultado.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFound.Value.Should().BeEquivalentTo(new { mensagem = "Usuário não possui notificações." });
        QueryMock.GarantirChamadaDeObterPorIdUsuarioAsync(usuarioId);
    }

    [Fact]
    public async Task ObterPorIdUsuario_QuandoExistemNotificacoes_DeveRetornarOkComLista()
    {
        // Arrange
        var usuarioId = 456;
        var notificacoes = NotificacaoFaker.ListaValida();
        QueryMock.ConfigurarObterPorIdUsuarioAsync(usuarioId, notificacoes);

        // Act
        var resultado = await Controller.ObterPorIdUsuario(usuarioId);

        // Assert
        var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(notificacoes);
        QueryMock.GarantirChamadaDeObterPorIdUsuarioAsync(usuarioId);
    }
}
