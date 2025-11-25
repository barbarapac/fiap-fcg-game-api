using System.Linq;
using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios;

public class UsuarioNotificationGatewayTest : UsuarioNotificationGatewayFixture
{
    [Fact]
    public async Task ObterUsuariosNotificaveisAsync_QuandoExistemUsuarios_DeveRetornarListaCorreta()
    {
        // Arrange
        var respostaGrpc = ObterUsuariosComNotificacoesResponseFaker.ComUsuarios(2);
        UsuarioServiceMock.ConfigurarResposta(respostaGrpc);

        // Act
        var resultado = await Gateway.ObterUsuariosNotificaveisAsync();

        // Assert
        resultado.Should().HaveCount(2);
        resultado.First().Id.Should().Be(respostaGrpc.Usuarios[0].Id);
        resultado.First().Email.Should().Be(respostaGrpc.Usuarios[0].Email);
        UsuarioServiceMock.GarantirMetodoChamado();
    }

    [Fact]
    public async Task ObterUsuariosNotificaveisAsync_QuandoNaoExistemUsuarios_DeveRetornarListaVazia()
    {
        // Arrange
        var respostaGrpc = ObterUsuariosComNotificacoesResponseFaker.Vazio();
        UsuarioServiceMock.ConfigurarResposta(respostaGrpc);

        // Act
        var resultado = await Gateway.ObterUsuariosNotificaveisAsync();

        // Assert
        resultado.Should().BeEmpty();
        UsuarioServiceMock.GarantirMetodoChamado();
    }
}