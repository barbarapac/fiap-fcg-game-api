using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Consultar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Consultar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Consultar;

public class ConsultarPromocaoControllerTest : ConsultarPromocaoControllerFixture
{
    [Fact]
    public async Task ListarTodas_QuandoChamado_DeveRetornarOkComLista()
    {
        // Arrange
        var lista = PromocaoResponseFaker.GerarLista(2);
        ConsultaPromocaoQueryMock.ConfigurarObterTodasAsync(lista);

        // Act
        var resultado = await Controller.ListarTodas();

        // Assert
        var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(lista);
    }

    [Fact]
    public async Task ObterPorId_QuandoPromocaoExistir_DeveRetornarOk()
    {
        // Arrange
        var promocao = PromocaoResponseFaker.Gerar();
        ConsultaPromocaoQueryMock.ConfigurarObterPorIdAsync(promocao);

        // Act
        var resultado = await Controller.ObterPorId(1);

        // Assert
        var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(promocao);
    }

    [Fact]
    public async Task ObterPorId_QuandoPromocaoNaoExistir_DeveRetornarNotFound()
    {
        // Arrange
        ConsultaPromocaoQueryMock.ConfigurarObterPorIdAsync(null);

        // Act
        var resultado = await Controller.ObterPorId(999);

        // Assert
        var notFound = resultado.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFound.Value.Should().BeEquivalentTo(new { mensagem = "Promoção não encontrada." });
    }
}