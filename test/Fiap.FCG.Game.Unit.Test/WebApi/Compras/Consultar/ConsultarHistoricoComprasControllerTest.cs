using Fiap.FCG.Game.Application.Compras.Consultar;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar
{
    public class ConsultarHistoricoComprasControllerTest : ConsultarHistoricoComprasControllerFixture
    {
        [Fact]
        public async Task ObterHistorico_QuandoHistoricoEncontrado_DeveRetornar200ComDados()
        {
            // Arrange
            var usuarioId = 123;
            var response = HistoricoCompraResponseFaker.ComCompras(usuarioId);
            var result = Result.Success(response);

            MediatorBuilder.SetupReturn<ConsultarHistoricoQuery, Result<HistoricoCompraResponse>>(
                q => q.UsuarioId == usuarioId,
                result
            );

            // Act
            var resultado = await Controller.ObterHistorico(usuarioId);

            // Assert
            resultado.Should().BeOfType<OkObjectResult>();

            var ok = resultado.As<OkObjectResult>();
            ok.StatusCode.Should().Be(200);

            ok.Value.Should().BeEquivalentTo(new
            {
                sucesso = true,
                compras = response
            });

            MediatorBuilder.VerifyCalled<ConsultarHistoricoQuery, Result<HistoricoCompraResponse>>(q => q.UsuarioId == usuarioId);
        }


        [Fact]
        public async Task ObterHistorico_QuandoNaoEncontrado_DeveRetornar404()
        {
            // Arrange
            var usuarioId = 999;
            var result = Result.Failure<HistoricoCompraResponse>("Nenhuma compra encontrada");

            MediatorBuilder.SetupReturn<ConsultarHistoricoQuery, Result<HistoricoCompraResponse>>(
                q => q.UsuarioId == usuarioId,
                result
            );

            // Act
            var resultado = await Controller.ObterHistorico(usuarioId);

            // Assert
            resultado.Should().BeOfType<NotFoundObjectResult>();
            var notFound = resultado.As<NotFoundObjectResult>();
            notFound.StatusCode.Should().Be(404);

            notFound.Value.Should().BeEquivalentTo(new
            {
                sucesso = false,
                mensagem = "Nenhuma compra encontrada para o usuário."
            });

            MediatorBuilder.VerifyCalled<ConsultarHistoricoQuery, Result<HistoricoCompraResponse>>(
                q => q.UsuarioId == usuarioId
            );
        }

    }
}
