using System;
using System.Linq;
using Fiap.FCG.Game.Domain.Notificacoes;
using Fiap.FCG.Game.Unit.Test.Domin.Notificacoes.Fakers;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Domin.Notificacoes;

public class NotificacaoTest
{
    [Fact]
    public void Criar_ComJogoEPromocao_DeveRetornarNotificacaoComTituloEMensagemEsperados()
    {
        // Arrange
        var jogo = JogoFaker.Valido();
        var promocao = PromocaoFaker.Valida();

        // Act
        var notificacao = Notificacao.Criar(jogo, promocao);

        // Assert
        notificacao.Titulo.Should().Be($"Promoção: {promocao.Nome}!");
        notificacao.Mensagem.Should().Be($"O jogo {jogo.Nome} está com {promocao.DescontoPercentual}% de desconto!");
        notificacao.DataEnvio.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        notificacao.Enviadas.Should().BeEmpty();
    }

    [Fact]
    public void AdicionarEnvio_QuandoChamado_DeveAdicionarNotificacaoEnviadaNaLista()
    {
        // Arrange
        var notificacao = Notificacao.Criar(JogoFaker.Valido(), PromocaoFaker.Valida());
        var usuarioId = 123;
        var promocaoJogoId = 456;

        // Act
        notificacao.AdicionarEnvio(usuarioId, promocaoJogoId);

        // Assert
        notificacao.Enviadas.Should().HaveCount(1);
        var envio = notificacao.Enviadas.First();
        envio.UsuarioId.Should().Be(usuarioId);
        envio.PromocaoJogoId.Should().Be(promocaoJogoId);
    }
}