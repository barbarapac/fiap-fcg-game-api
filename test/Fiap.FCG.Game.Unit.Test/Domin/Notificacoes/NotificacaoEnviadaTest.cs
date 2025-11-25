using Fiap.FCG.Game.Domain.Notificacoes;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Domin.Notificacoes;

public class NotificacaoEnviadaTest
{
    [Fact]
    public void Construtor_ComUsuarioIdEPromocaoJogoId_DeveInicializarCorretamente()
    {
        // Arrange
        var usuarioId = 10;
        var promocaoJogoId = 20;

        // Act
        var enviada = new NotificacaoEnviada(usuarioId, promocaoJogoId);

        // Assert
        enviada.UsuarioId.Should().Be(usuarioId);
        enviada.PromocaoJogoId.Should().Be(promocaoJogoId);
        enviada.NotificacaoId.Should().Be(0); 
        enviada.Notificacao.Should().BeNull();
        enviada.PromocaoJogo.Should().BeNull();
    }
}