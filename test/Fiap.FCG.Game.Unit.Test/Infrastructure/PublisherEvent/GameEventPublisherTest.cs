using System.Text.Json;
using System.Threading.Tasks;
using Fiap.FCG.Game.Infrastructure.PublisherEvent._Shared;
using Fiap.FCG.Game.Infrastructure.PublisherEvent.GameEvent;
using Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent;

public class GameEventPublisherTest : GameEventPublisherFixture
{
    [Fact]
    public async Task JogoCadastradoPublishAsync_QuandoChamado_DeveEnviarMensagemCorreta()
    {
        // Arrange
        var jogo = JogoFaker.Valido();

        // Act
        await Publisher.JogoCadastradoPublishAsync(jogo);

        // Assert
        var mensagem = ServiceBusSenderMock.ObterUltimaMensagemEnviada();
        var evento = JsonSerializer.Deserialize<GameEvent>(mensagem.Body.ToString());

        evento.Should().NotBeNull();
        evento!.Tipo.Should().Be(TipoEvento.JOGO_CADASTADO);
        evento.JogoId.Should().Be(jogo.Id);
        evento.Nome.Should().Be(jogo.Nome);
        evento.Preco.Should().Be(jogo.Preco);

        ServiceBusSenderMock.GarantirMensagemEnviada();
    }

    [Fact]
    public async Task JogoEditadoPublishAsync_QuandoChamado_DeveEnviarMensagemCorreta()
    {
        // Arrange
        var jogo = JogoFaker.Valido();

        // Act
        await Publisher.JogoEditadoPublishAsync(jogo);

        // Assert
        var mensagem = ServiceBusSenderMock.ObterUltimaMensagemEnviada();
        var evento = JsonSerializer.Deserialize<GameEvent>(mensagem.Body.ToString());

        evento.Should().NotBeNull();
        evento!.Tipo.Should().Be(TipoEvento.JOGO_ATUALIZADO);
        evento.JogoId.Should().Be(jogo.Id);
        evento.Nome.Should().Be(jogo.Nome);
        evento.Preco.Should().Be(jogo.Preco);

        ServiceBusSenderMock.GarantirMensagemEnviada();
    }
}