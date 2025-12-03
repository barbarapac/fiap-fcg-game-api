using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Fakers;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar;

public class AtualizarPromocaoHandlerTest : AtualizarPromocaoHandlerFixture
{
    [Fact]
    public async Task Handle_QuandoPromocaoNaoEncontrada_DeveRetornarErro()
    {
        // Arrange
        var command = AtualizarPromocaoCommandFaker.Valido();
        PromocaoRepositoryMock.ConfigurarObterPorId(null);

        // Act
        var result = await Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Be("Promoção não encontrada.");
        PromocaoEventPublisherMock.GarantirPromocaoEditadaPublishAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_QuandoJogosInexistentes_DeveRetornarErro()
    {
        // Arrange
        var command  = AtualizarPromocaoCommandFaker.Valido();
        var promocao = PromocaoFaker.Valida();

        PromocaoRepositoryMock.ConfigurarObterPorId(promocao);
        JogoRepositoryMock.ConfigurarObterAsync(command.JogosIds!, new());

        // Act
        var result = await Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Be("Um ou mais jogos informados não existem.");
        PromocaoEventPublisherMock.GarantirPromocaoEditadaPublishAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_QuandoHaConflito_DeveRetornarErroComIds()
    {
        // Arrange
        var command   = AtualizarPromocaoCommandFaker.Valido();
        var promocao  = PromocaoFaker.Valida();
        var conflitos = PromocaoJogoFaker.ComConflito(99, command.JogosIds!);

        PromocaoRepositoryMock.ConfigurarObterPorId(promocao);
        JogoRepositoryMock.ConfigurarObterAsync(command.JogosIds!, PromocaoFaker.JogosValidos(command.JogosIds!));
        PromocaoRepositoryMock.ConfigurarObterPorJogosIds(conflitos);

        // Act
        var result = await Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Be($"Os jogos {string.Join(", ", command.JogosIds!)} já estão vinculados a outras promoções.");
        PromocaoEventPublisherMock.GarantirPromocaoEditadaPublishAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_QuandoDadosValidos_DeveAtualizarComSucessoEPublicarEvento()
    {
        // Arrange
        var command     = AtualizarPromocaoCommandFaker.Valido();
        var promocao    = PromocaoFaker.Valida();
        var jogos       = PromocaoFaker.JogosValidos(command.JogosIds!);
        var semConflito = PromocaoJogoFaker.SemConflito(command.Id, command.JogosIds!);

        PromocaoRepositoryMock.ConfigurarObterPorId(promocao);
        JogoRepositoryMock.ConfigurarObterAsync(command.JogosIds!, jogos);
        PromocaoRepositoryMock.ConfigurarObterPorJogosIds(semConflito);
        PromocaoRepositoryMock.ConfigurarAtualizarAsync(Result.Success("Atualizado"));
        PromocaoEventPublisherMock.ConfigurarPromocaoEditadaPublishAsync();

        // Act
        var result = await Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Sucesso.Should().BeTrue();
        result.Valor.Should().Be("Atualizado");
        PromocaoEventPublisherMock.GarantirPromocaoEditadaPublishAsyncChamado(promocao);
    }
}
