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
    public class ObterHistoricoComprasControllerTests : ObterHistoricoComprasControllerFixture
    {
        //[Fact(DisplayName = "ObterHistorico deve retornar 200 quando existir histórico de compras")]
        //public async Task ObterHistorico_DeveRetornarOk_QuandoExistirHistorico()
        //{
        //    // Arrange
        //    var usuarioId = 20;
        //    var request = new ConsultarHistoricoComprasQuery(usuarioId);
        //    var expected = HistoricoCompraFaker.GerarLista(2);

        //    MediatorMock.SetupReturn(request, expected);

        //    // Act
        //    var response = await Controller.ObterHistorico(usuarioId);

        //    // Assert
        //    response.Should().BeOfType<OkObjectResult>();

        //    var ok = response as OkObjectResult;
        //    ok!.Value.Should().BeEquivalentTo(new
        //    {
        //        sucesso = true,
        //        compras = expected
        //    });

        //    MediatorMock.VerifyCalled<ConsultarHistoricoComprasQuery, List<HistoricoCompra>>(request);
        //}

        //[Fact(DisplayName = "ObterHistorico deve retornar 404 quando histórico estiver vazio")]
        //public async Task ObterHistorico_DeveRetornarNotFound_QuandoVazio()
        //{
        //    // Arrange
        //    var usuarioId = 50;
        //    var request = new ConsultarHistoricoComprasQuery(usuarioId);
        //    var expected = new List<HistoricoCompra>();

        //    MediatorMock.SetupReturn(request, expected);

        //    // Act
        //    var response = await Controller.ObterHistorico(usuarioId);

        //    // Assert
        //    response.Should().BeOfType<NotFoundObjectResult>();

        //    var notFound = response as NotFoundObjectResult;
        //    notFound!.Value.Should().BeEquivalentTo(new
        //    {
        //        sucesso = false,
        //        mensagem = "Nenhuma compra encontrada para o usuário."
        //    });

        //    MediatorMock.VerifyCalled<ConsultarHistoricoComprasQuery, List<HistoricoCompra>>(request);
        //}

        //[Fact(DisplayName = "ObterHistorico deve retornar 404 quando Mediator retornar null")]
        //public async Task ObterHistorico_DeveRetornarNotFound_QuandoNull()
        //{
        //    // Arrange
        //    var usuarioId = 99;
        //    var request = new ConsultarHistoricoComprasQuery(usuarioId);

        //    MediatorMock.SetupReturn<ConsultarHistoricoComprasQuery, List<HistoricoCompra>?>(request, null);

        //    // Act
        //    var response = await Controller.ObterHistorico(usuarioId);

        //    // Assert
        //    response.Should().BeOfType<NotFoundObjectResult>();

        //    MediatorMock.VerifyCalled<ConsultarHistoricoComprasQuery, List<HistoricoCompra>?>(request);
        //}
    }
}
