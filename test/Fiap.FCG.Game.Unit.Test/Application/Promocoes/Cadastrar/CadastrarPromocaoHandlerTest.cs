using System.Collections.Generic;
using System.Threading.Tasks;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Cadastrar.Fakers;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Cadastrar.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Cadastrar;

public class CadastrarPromocaoHandlerTest : CadastrarPromocaoHandlerFixture
{
    [Fact]
    public async Task Handle_QuandoPromocaoJaExiste_DeveRetornarErro()
    {
        // Arrange
        var command = CadastrarPromocaoCommandFaker.Valido();
        PromocaoRepositoryMock.ConfigurarExisteAsync(command.Nome, true);

        // Act
        var result = await Handler.Handle(command, default);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Be("Promoção já cadastrada.");
        PromocaoEventPublisherMock.GarantirPromocaoCadastradaPublishAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_QuandoPromocaoInvalida_DeveRetornarErro()
    {
        // Arrange
        var command = CadastrarPromocaoCommandFaker.SemNomePromocao();
        PromocaoRepositoryMock.ConfigurarExisteAsync(command.Nome, false);

        // Act
        var result = await Handler.Handle(command, default);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().NotBeNullOrWhiteSpace();
        PromocaoEventPublisherMock.GarantirPromocaoCadastradaPublishAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_QuandoSemJogosInformados_DeveRetornarErro()
    {
        // Arrange
        var command = CadastrarPromocaoCommandFaker.SemJogos();
        PromocaoRepositoryMock.ConfigurarExisteAsync(command.Nome, false);

        // Act
        var result = await Handler.Handle(command, default);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Be("Promoção deve conter pelo menos um jogo.");
        PromocaoEventPublisherMock.GarantirPromocaoCadastradaPublishAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_QuandoJogoInexistente_DeveRetornarErro()
    {
        // Arrange
        var command = CadastrarPromocaoCommandFaker.Valido();
        PromocaoRepositoryMock.ConfigurarExisteAsync(command.Nome, false);
        JogoRepositoryMock.ConfigurarObterAsync(command.JogosIds, new List<Jogo>()); 

        // Act
        var result = await Handler.Handle(command, default);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Be("Um ou mais jogos informados não existem.");
        PromocaoEventPublisherMock.GarantirPromocaoCadastradaPublishAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_QuandoPossuiPromocaoJaCadastrado_DeveRetornarErro()
    {
        // Arrange
        var command = CadastrarPromocaoCommandFaker.Valido();
        var jogos = PromocaoFaker.JogosValidos(command.JogosIds);

        PromocaoRepositoryMock.ConfigurarExisteAsync(command.Nome, false);
        PromocaoRepositoryMock.JogoPossuiPromocaoCadastrada();
        JogoRepositoryMock.ConfigurarObterAsync(command.JogosIds, jogos);

        // Act
        var result = await Handler.Handle(command, default);

        // Assert
        result.Sucesso.Should().BeFalse();
        PromocaoEventPublisherMock.GarantirPromocaoCadastradaPublishAsyncNaoChamado();
    }

    //[Fact]
    //public async Task Handle_QuandoValido_DeveCadastrarComSucessoEPublicarEvento()
    //{
    //    // Arrange
    //    var command = CadastrarPromocaoCommandFaker.Valido();
    //    var jogos = PromocaoFaker.JogosValidos(command.JogosIds);

    //    PromocaoRepositoryMock.ConfigurarExisteAsync(command.Nome, false);
    //    PromocaoRepositoryMock.JogoNaoPossuiPromocaoCadastrada();
    //    JogoRepositoryMock.ConfigurarObterAsync(command.JogosIds, jogos);
    //    PromocaoEventPublisherMock.ConfigurarPromocaoCadastradaPublishAsync();

    //    // Act
    //    var result = await Handler.Handle(command, default);

    //    // Assert
    //    result.Sucesso.Should().BeTrue();
    //    result.Valor.Should().Be(command.Nome);

    //    PromocaoRepositoryMock.GarantirAdicao();
    //    PromocaoEventPublisherMock.GarantirPromocaoCadastradaPublishAsyncChamado();
    //}
}
