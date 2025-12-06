using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios;

public class UsuarioNotificationGatewayTest : UsuarioNotificationGatewayFixture
{
    [Fact]
    public async Task ObterUsuariosNotificaveisGrpcAsync_QuandoExistemUsuarios_DeveRetornarListaCorreta()
    {
        // Arrange
        var respostaGrpc = ObterUsuariosComNotificacoesResponseFaker.ComUsuarios(2);
        UsuarioServiceMock.ConfigurarResposta(respostaGrpc);

        // Act
        var resultado = await Gateway.ObterUsuariosNotificaveisGrpcAsync();

        // Assert
        resultado.Should().HaveCount(2);
        resultado.First().Id.Should().Be(respostaGrpc.Usuarios[0].Id);
        UsuarioServiceMock.GarantirMetodoChamado();
    }

    [Fact]
    public async Task ObterUsuariosNotificaveisGrpcAsync_QuandoNaoExistemUsuarios_DeveRetornarListaVazia()
    {
        // Arrange
        var respostaGrpc = ObterUsuariosComNotificacoesResponseFaker.Vazio();
        UsuarioServiceMock.ConfigurarResposta(respostaGrpc);

        // Act
        var resultado = await Gateway.ObterUsuariosNotificaveisGrpcAsync();

        // Assert
        resultado.Should().BeEmpty();
        UsuarioServiceMock.GarantirMetodoChamado();
    }

    [Fact]
    public async Task ObterUsuariosNotificaveisHttpAsync_QuandoUsuariosExistem_DeveRetornarLista()
    {
        // Arrange
        var esperados = new List<UsuarioNotificavelDto>
        {
            new() { Id = 1, Nome = "Nome1", Email = "email1@dominio.com" },
            new() { Id = 2, Nome = "Nome2", Email = "email2@dominio.com" }
        };

        HttpClientMock.ConfigurarGetComRetorno(esperados);

        // Act
        var resultado = await Gateway.ObterUsuariosNotificaveisHttpAsync();

        // Assert
        resultado.Should().HaveCount(2);
        resultado.Should().BeEquivalentTo(esperados);
        HttpClientMock.GarantirRequisicaoGetFoiFeita();
    }

    [Fact]
    public async Task ObterUsuariosNotificaveisHttpAsync_QuandoRetornoEhVazio_DeveRetornarListaVazia()
    {
        // Arrange
        HttpClientMock.ConfigurarGetComRetorno<IList<UsuarioNotificavelDto>>(null);

        // Act
        var resultado = await Gateway.ObterUsuariosNotificaveisHttpAsync();

        // Assert
        resultado.Should().BeEmpty();
        HttpClientMock.GarantirRequisicaoGetFoiFeita();
    }

    [Fact]
    public async Task ObterUsuariosNotificaveisHttpAsync_QuandoErroNaRequisicao_DeveLancarExcecao()
    {
        // Arrange
        HttpClientMock.ConfigurarGetComErro();

        // Act
        var acao = async () => await Gateway.ObterUsuariosNotificaveisHttpAsync();

        // Assert
        await acao.Should().ThrowAsync<HttpRequestException>();
        HttpClientMock.GarantirRequisicaoGetFoiFeita();
    }
}
