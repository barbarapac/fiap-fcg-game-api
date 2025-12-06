using System.Threading.Tasks;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Comprar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Comprar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Comprar
{
    public class ComprarJogoControllerTests : ComprarJogoControllerFixture
    {
        [Fact(DisplayName = "Comprar deve retornar Ok(200) quando a compra for realizada com sucesso")]
        public async Task Comprar_DeveRetornarOk_QuandoSucesso()
        {
            // Arrange
            var command = ComprarJogoCommandFaker.GerarValido();
            var expectedResult = Result<bool>.Success(true);

            MediatorMock.SetupReturn(command, expectedResult);

            // Act
            var result = await Controller.Comprar(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var ok = result as OkObjectResult;
            ok!.Value.Should().BeEquivalentTo(new
            {
                sucesso = true,
                mensagem = "Compra realizada com sucesso.",
                valor = true
            });

            MediatorMock.VerifyCalled(command);
        }

        [Fact(DisplayName = "Comprar deve retornar BadRequest(400) quando a compra falhar")]
        public async Task Comprar_DeveRetornarBadRequest_QuandoFalhar()
        {
            // Arrange
            var command = ComprarJogoCommandFaker.GerarValido();
            var expectedResult = Result<bool>.Failure("Saldo insuficiente");

            MediatorMock.SetupReturn(command, expectedResult);

            // Act
            var result = await Controller.Comprar(command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var bad = result as BadRequestObjectResult;
            bad!.Value.Should().BeEquivalentTo(new
            {
                sucesso = false,
                mensagem = "Saldo insuficiente",
                valor = false
            });

            MediatorMock.VerifyCalled(command);
        }
    }
}
