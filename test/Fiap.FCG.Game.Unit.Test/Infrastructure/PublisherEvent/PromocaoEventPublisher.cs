using System.Text.Json;
using System.Threading.Tasks;
using Fiap.FCG.Game.Infrastructure.PublisherEvent._Shared;
using Fiap.FCG.Game.Infrastructure.PublisherEvent.PromocaoEvent;
using Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent;

public class PromocaoEventPublisherTest : PromocaoEventPublisherFixture
{
    [Fact]
    public async Task PromocaoCadastradaPublishAsync_DeveEnviarEventoCorreto()
    {
        // Arrange
        var promocao = PromocaoFaker.Valida();

        // Act
        await Publisher.PromocaoCadastradaPublishAsync(promocao);

        // Assert
        var mensagem = ServiceBusSenderMock.ObterUltimaMensagemEnviada();
        var evento = JsonSerializer.Deserialize<PromocaoEvent>(mensagem.Body.ToString());

        evento.Should().NotBeNull();
        evento!.Tipo.Should().Be(TipoEvento.PROMOCAO_CADASTRADA);
        evento.PromocaoId.Should().Be(promocao.Id);
        evento.Titulo.Should().Be(promocao.Nome);
        evento.Desconto.Should().Be(promocao.DescontoPercentual);

        ServiceBusSenderMock.GarantirMensagemEnviada();
    }

    [Fact]
    public async Task PromocaoEditadaPublishAsync_DeveEnviarEventoCorreto()
    {
        // Arrange
        var promocao = PromocaoFaker.Valida();

        // Act
        await Publisher.PromocaEditadaPublishAsync(promocao);

        // Assert
        var mensagem = ServiceBusSenderMock.ObterUltimaMensagemEnviada();
        var evento = JsonSerializer.Deserialize<PromocaoEvent>(mensagem.Body.ToString());

        evento!.Tipo.Should().Be(TipoEvento.PROMOCAO_ATUALIZADA);
        evento.PromocaoId.Should().Be(promocao.Id);

        ServiceBusSenderMock.GarantirMensagemEnviada();
    }

    [Fact]
    public async Task PromocaoRemovidaPublishAsync_DeveEnviarEventoCorreto()
    {
        // Arrange
        var promocao = PromocaoFaker.Valida();

        // Act
        await Publisher.PromocaoRemovidaPublishAsync(promocao);

        // Assert
        var mensagem = ServiceBusSenderMock.ObterUltimaMensagemEnviada();
        var evento = JsonSerializer.Deserialize<PromocaoEvent>(mensagem.Body.ToString());

        evento!.Tipo.Should().Be(TipoEvento.PROMOCAO_DELETADA);
        evento.PromocaoId.Should().Be(promocao.Id);

        ServiceBusSenderMock.GarantirMensagemEnviada();
    }
}