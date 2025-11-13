using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test._Shared;
using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Cadastrar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Cadastrar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Cadastrar;

public class CadastrarJogoControllerTest : CadastrarJogoControllerFixture
{
    [Fact]
    public async Task Cadastrar_QuandoSucesso_DeveRetornarOk()
    {
        // Arrange
        var comando = CadastrarJogoCommandFaker.Valido();
        var resultado = ResultFaker.Sucesso("JOGO123");

        MediatorMock.ConfigurarEnvio(comando, resultado);

        // Act
        var response = await Controller.Cadastrar(comando);

        // Assert
        var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(new
        {
            sucesso = true,
            mensagem = "Jogo cadastrado com sucesso.",
            valor = "JOGO123"
        });

        MediatorMock.GarantirEnvio(comando);
    }

    [Fact]
    public async Task Cadastrar_QuandoFalha_DeveRetornarBadRequest()
    {
        // Arrange
        var comando = CadastrarJogoCommandFaker.Valido();
        var resultado = ResultFaker.Falha("Erro ao cadastrar jogo");

        MediatorMock.ConfigurarEnvio(comando, resultado);

        // Act
        var response = await Controller.Cadastrar(comando);

        // Assert
        var badRequest = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.Value.Should().BeEquivalentTo(new
        {
            sucesso = false,
            mensagem = "Erro ao cadastrar jogo",
            valor = (string)null
        });

        MediatorMock.GarantirEnvio(comando);
    }
}
