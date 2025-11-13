using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar;

public class ConsultarJogoControllerTest : ConsultarJogoControllerFixture
{
    [Fact]
    public async Task ObterPorId_QuandoJogoExiste_DeveRetornarOk()
    {
        // Arrange
        var jogo = JogoResponseFaker.Valido();
        ConsultaMock.ConfigurarObterPorId(jogo.Id, jogo);

        // Act
        var resultado = await Controller.ObterPorId(jogo.Id);

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(new { sucesso = true, jogo });

        ConsultaMock.GarantirObterPorIdChamado();
    }

    [Fact]
    public async Task ObterPorId_QuandoJogoNaoExiste_DeveRetornarNotFound()
    {
        // Arrange
        const int id = 999;
        ConsultaMock.ConfigurarObterPorId(id, null);

        // Act
        var resultado = await Controller.ObterPorId(id);

        // Assert
        var notFound = resultado.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFound.Value.Should().BeEquivalentTo(new 
        { 
            sucesso = false, 
            mensagem = "Jogo não encontrado." 
        });

        ConsultaMock.GarantirObterPorIdChamado();
    }

    [Fact]
    public async Task ObterTodos_QuandoJogosExistem_DeveRetornarOk()
    {
        // Arrange
        var jogos = JogoResponseFaker.Lista();
        ConsultaMock.ConfigurarObterTodos(jogos);

        // Act
        var resultado = await Controller.ObterTodos();

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(new 
        { 
            sucesso = true, 
            jogos 
        });

        ConsultaMock.GarantirObterTodosChamado();
    }
}