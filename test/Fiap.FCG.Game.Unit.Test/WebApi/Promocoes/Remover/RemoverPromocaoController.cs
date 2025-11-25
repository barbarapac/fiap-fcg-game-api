using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Promocoes.Remover;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Unit.Test._Shared;
using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Remover.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Remover.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Remover
{
    public class RemoverPromocaoControllerTest : RemoverPromocaoControllerFixture
    {
        [Fact]
        public async Task Cadastrar_QuandoRemocaoForBemSucedida_DeveRetornarOk()
        {
            // Arrange
            var command = RemoverPromocaoCommandFaker.ComIdValido();
            var result  = ResultFaker.Sucesso("Promoção removida com sucesso.");

            MediatorMock.ConfigurarSend(command, result);

            // Act
            var resultado = await Controller.Cadastrar(command) as OkObjectResult;

            // Assert
            resultado.Should().NotBeNull();
            MediatorMock.GarantirSend<RemoverPromocaoCommand, Result<string>>(x => x.PromocaoId == command.PromocaoId);

        }

        [Fact]
        public async Task Cadastrar_QuandoRemocaoFalhar_DeveRetornarBadRequest()
        {
            // Arrange
            var command = RemoverPromocaoCommandFaker.ComIdValido();
            var result = ResultFaker.Falha("Promoção não encontrada.");
            
            MediatorMock.ConfigurarSend(command, result);

            // Act
            var resultado = await Controller.Cadastrar(command) as BadRequestObjectResult;

            // Assert
            resultado.Should().NotBeNull();
            MediatorMock.GarantirSend<RemoverPromocaoCommand, Result<string>>(x => x.PromocaoId == command.PromocaoId);
        }
    }
}
