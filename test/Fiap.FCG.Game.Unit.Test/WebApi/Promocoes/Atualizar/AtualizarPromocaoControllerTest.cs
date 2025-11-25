using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Promocoes.Atualizar;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Unit.Test._Shared;
using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Atualizar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Atualizar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Atualizar;

public class AtualizarPromocaoControllerTest : AtualizarPromocaoControllerFixture
{
    [Fact]
    public async Task Atualizar_QuandoAtualizacaoForBemSucedida_DeveRetornarOk()
    {
        // Arrange
        var command = AtualizarPromocaoCommandFaker.Valido();
        var result  = ResultFaker.Sucesso("promoção123");

        MediatorMock.ConfigurarSend(command, result);
        // Act
        var response = await Controller.Atualizar(command) as OkObjectResult;

        // Assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(200);
        MediatorMock.GarantirSend<AtualizarPromocaoCommand, Result<string>>(x => x.Nome == command.Nome);
    }

    [Fact]
    public async Task Atualizar_QuandoAtualizacaoFalhar_DeveRetornarBadRequest()
    {
        // Arrange
        var command = AtualizarPromocaoCommandFaker.Valido();
        var result  = ResultFaker.Falha("Erro ao atualizar");

        MediatorMock.ConfigurarSend(command, result);

        // Act
        var response = await Controller.Atualizar(command) as BadRequestObjectResult;

        // Assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(400);
        MediatorMock.GarantirSend<AtualizarPromocaoCommand, Result<string>>(x => x.Nome == command.Nome);
    }
}