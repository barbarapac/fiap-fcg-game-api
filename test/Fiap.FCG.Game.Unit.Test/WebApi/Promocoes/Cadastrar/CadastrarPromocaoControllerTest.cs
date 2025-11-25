using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Promocoes.Cadastar;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Unit.Test._Shared;
using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Cadastrar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Cadastrar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Cadastrar;

public class CadastrarPromocaoControllerTest : CadastrarPromocaoControllerFixture
{
    [Fact]
    public async Task Cadastrar_QuandoSucesso_DeveRetornarOk()
    {
        // Arrange
        var command = CadastrarUsuarioCommandFaker.Valido();
        var result  = ResultFaker.Sucesso("PROMO123");

        MediatorMock.ConfigurarSend(command, result);

        // Act
        var response = await Controller.Cadastrar(command);

        // Assert
        var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(new
        {
            sucesso = true,
            mensagem = "Promoção cadastrada com sucesso.",
            valor = "PROMO123"
        });

        MediatorMock.GarantirSend<CadastrarPromocaoCommand, Result<string>>(x => x.Nome == command.Nome);
    }

    [Fact]
    public async Task Cadastrar_QuandoFalha_DeveRetornarBadRequest()
    {
        // Arrange
        var command = CadastrarUsuarioCommandFaker.Valido();
        var result  = ResultFaker.Falha("Erro ao cadastrar promoção");
        
        MediatorMock.ConfigurarSend(command, result);
        
        // Act
        var response = await Controller.Cadastrar(command);

        // Assert
        var badRequest = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.Value.Should().BeEquivalentTo(new
        {
            sucesso = false,
            mensagem = "Erro ao cadastrar promoção",
            valor = (string)null
        });

        MediatorMock.GarantirSend<CadastrarPromocaoCommand, Result<string>>(x => x.Nome == command.Nome);
    }
}