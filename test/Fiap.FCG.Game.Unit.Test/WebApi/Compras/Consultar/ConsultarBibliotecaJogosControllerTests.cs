using System.Collections.Generic;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Compras.Consultar;
using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar
{
    public class ConsultarBibliotecaJogosControllerTests : ConsultarBibliotecaJogosControllerFixture
    {
        //[Fact(DisplayName = "ListarJogosAdquiridos deve retornar 200 quando usuário possuir jogos")]
        //public async Task ListarJogosAdquiridos_DeveRetornarOk_QuandoExistirJogos()
        //{
        //    // Arrange
        //    var usuarioId = 10;
        //    var request = new ConsultarBibliotecaQuery(usuarioId);
        //    var expected = BibliotecaJogoFaker.GerarLista(3);

        //    MediatorMock.SetupReturn(request, expected);

        //    // Act
        //    var response = await Controller.ListarJogosAdquiridos(usuarioId);

        //    // Assert
        //    response.Should().BeOfType<OkObjectResult>();
        //    var okResult = response as OkObjectResult;

        //    okResult!.Value.Should().BeEquivalentTo(new
        //    {
        //        sucesso = true,
        //        jogos = expected
        //    });

        //    MediatorMock.VerifyCalled<ConsultarBibliotecaQuery, List<BibliotecaJogo>>(request);
        //}



        //[Fact(DisplayName = "ListarJogosAdquiridos deve retornar 404 quando usuário não possuir jogos")]
        //public async Task ListarJogosAdquiridos_DeveRetornarNotFound_QuandoListaEstiverVazia()
        //{
        //    // Arrange
        //    var usuarioId = 20;
        //    var request = new ConsultarBibliotecaQuery(usuarioId);
        //    var expected = new List<BibliotecaJogo>();

        //    MediatorMock.SetupReturn(request, expected);

        //    // Act
        //    var response = await Controller.ListarJogosAdquiridos(usuarioId);

        //    // Assert
        //    response.Should().BeOfType<NotFoundObjectResult>();
        //    var notFoundResult = response as NotFoundObjectResult;

        //    notFoundResult!.Value.Should().BeEquivalentTo(new
        //    {
        //        sucesso = false,
        //        mensagem = "Nenhum jogo encontrado para o usuário."
        //    });

        //    MediatorMock.VerifyCalled<ConsultarBibliotecaQuery, List<BibliotecaJogo>>(request);
        //}


        //[Fact(DisplayName = "ListarJogosAdquiridos deve retornar 404 quando Mediator retornar null")]
        //public async Task ListarJogosAdquiridos_DeveRetornarNotFound_QuandoRetornoNulo()
        //{
        //    // Arrange
        //    var usuarioId = 30;
        //    var request = new ConsultarBibliotecaQuery(usuarioId);

        //    MediatorMock.SetupReturn<ConsultarBibliotecaQuery, List<BibliotecaJogo>?>(request, null);

        //    // Act
        //    var response = await Controller.ListarJogosAdquiridos(usuarioId);

        //    // Assert
        //    response.Should().BeOfType<NotFoundObjectResult>();
        //    var notFoundResult = response as NotFoundObjectResult;

        //    notFoundResult!.Value.Should().BeEquivalentTo(new
        //    {
        //        sucesso = false,
        //        mensagem = "Nenhum jogo encontrado para o usuário."
        //    });

        //    MediatorMock.VerifyCalled<ConsultarBibliotecaQuery, List<BibliotecaJogo>>(request);
        //}
    }
}
