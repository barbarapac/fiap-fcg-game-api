using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test._Shared;
using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Atualizar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Atualizar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Atualizar;

public class AtualizarJogoControllerTest : AtualizarJogoControllerFixture
{
    [Fact]
    public async Task Atualizar_QuandoComandoValido_DeveRetornarOk()
    {
        // Arrange
        var comando = AtualizarJogoCommandFaker.Valido();
        var resultado = ResultFaker.Sucesso();
        
        MediatorMock.ConfigurarEnvio(comando, resultado);

        // Act
        var response = await Controller.Atualizar(comando);

        // Assert
        var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(new
        {
            sucesso = true
        });

        MediatorMock.GarantirEnvio(comando);
    }

    [Fact]
    public async Task Atualizar_QuandoComandoInvalido_DeveRetornarBadRequest()
    {
        // Arrange
        var comando = AtualizarJogoCommandFaker.Valido();
        var resultado = ResultFaker.FalhaBool("Erro ao atualizar jogo");
            
        MediatorMock.ConfigurarEnvio(comando, resultado);
        
        // Act
        var response = await Controller.Atualizar(comando);

        // Assert
        var badRequest = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.Value.Should().BeEquivalentTo(new
        {
            sucesso = false
        });

        MediatorMock.GarantirEnvio(comando);
    }
}