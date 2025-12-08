using Fiap.FCG.Game.Application.Compras.Consultar;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar
{
    public class ConsultarBibliotecaJogosControllerTest : ConsultarBibliotecaJogosControllerFixture
    {
        [Fact]
        public async Task ListarJogosAdquiridos_QuandoJogosEncontrados_DeveRetornar200ComDados()
        {
            // Arrange
            var usuarioId = 101;

            var jogos = new List<JogoAdquiridoResponse>
            {
                new JogoAdquiridoResponse { Nome = "FIFA 24" },
                new JogoAdquiridoResponse { Nome = "Elden Ring" }
            };

            var result = Result.Success(jogos);

            MediatorMock.SetupReturn<ConsultarBibliotecaQuery, Result<List<JogoAdquiridoResponse>>>(
                q => q.UsuarioId == usuarioId,
                result
            );

            // Act
            var response = await Controller.ListarJogosAdquiridos(usuarioId);

            // Assert
            response.Should().BeOfType<OkObjectResult>();

            var ok = response.As<OkObjectResult>();
            ok.StatusCode.Should().Be(200);

            ok.Value.Should().BeEquivalentTo(new
            {
                sucesso = true,
                jogos
            });

            MediatorMock.VerifyCalled<ConsultarBibliotecaQuery, Result<List<JogoAdquiridoResponse>>>(
                q => q.UsuarioId == usuarioId
            );
        }

        [Fact]
        public async Task ListarJogosAdquiridos_QuandoNaoEncontrado_DeveRetornar404()
        {
            // Arrange
            var usuarioId = 999;

            var result = Result.Success(new List<JogoAdquiridoResponse>());

            MediatorMock.SetupReturn<ConsultarBibliotecaQuery, Result<List<JogoAdquiridoResponse>>>(
                q => q.UsuarioId == usuarioId,
                result
            );

            // Act
            var response = await Controller.ListarJogosAdquiridos(usuarioId);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();

            var notFound = response.As<NotFoundObjectResult>();
            notFound.StatusCode.Should().Be(404);

            notFound.Value.Should().BeEquivalentTo(new
            {
                sucesso = false,
                mensagem = "Nenhum jogo encontrado para o usuário."
            });

            MediatorMock.VerifyCalled<ConsultarBibliotecaQuery, Result<List<JogoAdquiridoResponse>>>(
                q => q.UsuarioId == usuarioId
            );
        }

    }
}
