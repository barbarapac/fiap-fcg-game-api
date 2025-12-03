using System.Threading.Tasks;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover.Fakers;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover.Fixtures;
using FluentAssertions;
using Xunit;
using PromocaoFaker = Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Fakers.PromocaoFaker;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover;

public class RemoverPromocaoHandlerTest : RemoverPromocaoHandlerFixture
{
    [Fact]
    public async Task Handle_QuandoPromocaoNaoExiste_DeveRetornarFalha()
    {
        // Arrange
        var comando = RemoverPromocaoCommandFaker.ComIdValido();
        PromocaoRepositoryMock.ConfigurarObterPorId(null);

        // Act
        var result = await Handler.Handle(comando, default);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Be("A promoção informada não existe");
        PromocaoRepositoryMock.GarantirConsultaPorId(comando.PromocaoId);
        PromocaoEventPublisherMock.GarantirPromocaoRemovidaPublishAsyncChamado(null); // aceita null pois é isso que o handler envia
    }

    [Fact]
    public async Task Handle_QuandoRemocaoBemSucedida_DeveRetornarSucesso()
    {
        // Arrange
        var comando = RemoverPromocaoCommandFaker.ComIdValido();
        var promocao = PromocaoFaker.Valida();
        PromocaoRepositoryMock.ConfigurarObterPorId(promocao);
        PromocaoRepositoryMock.ConfigurarExcluir(promocao, Result.Success("removido"));
        PromocaoEventPublisherMock.ConfigurarPromocaoRemovidaPublishAsync();

        // Act
        var result = await Handler.Handle(comando, default);

        // Assert
        result.Sucesso.Should().BeTrue();
        result.Valor.Should().Be("removido");

        PromocaoRepositoryMock.GarantirConsultaPorId(comando.PromocaoId);
        PromocaoRepositoryMock.GarantirExclusao(promocao);
        PromocaoEventPublisherMock.GarantirPromocaoRemovidaPublishAsyncChamado(promocao);
    }

    [Fact]
    public async Task Handle_QuandoRemocaoFalha_DeveRetornarErro()
    {
        // Arrange
        var comando = RemoverPromocaoCommandFaker.ComIdValido();
        var promocao = PromocaoFaker.Valida();
        PromocaoRepositoryMock.ConfigurarObterPorId(promocao);
        PromocaoRepositoryMock.ConfigurarExcluir(promocao, Result.Failure<string>("erro ao excluir"));
        PromocaoEventPublisherMock.ConfigurarPromocaoRemovidaPublishAsync();

        // Act
        var result = await Handler.Handle(comando, default);

        // Assert
        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Be("erro ao excluir");

        PromocaoRepositoryMock.GarantirExclusao(promocao);
        PromocaoEventPublisherMock.GarantirPromocaoRemovidaPublishAsyncChamado(promocao);
    }
}
