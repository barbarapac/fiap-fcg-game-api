using Fiap.FCG.Game.Application.Compras.Consultar;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fakers;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fixtures;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar
{
    public class ConsultarHistoricoHandlerTest : ConsultarHistoricoHandlerFixture
    {
        [Fact]
        public async Task Handle_QuandoHistoricoEncontrado_DeveRetornarResponseComCompras()
        {
            // Arrange
            var usuarioId = 123;
            var historico = HistoricoCompraFaker.ValidoComJogos(usuarioId, 2);
            RepositoryMock.ConfigurarObterPorUsuario(new List<Domain.Compras.HistoricoCompra> { historico });

            var query = new ConsultarHistoricoQuery(usuarioId);

            // Act
            var resultado = await Handler.Handle(query, CancellationToken.None);

            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Valor.Compras.Should().HaveCount(historico.Itens.Count);

            foreach (var dto in resultado.Valor.Compras)
            {
                dto.ValorComDesconto.Should().BeLessThan(dto.ValorBase);
                dto.NomeJogo.Should().NotBeNullOrWhiteSpace();
            }

            RepositoryMock.GarantirObterPorUsuarioChamadoCom(usuarioId);
        }

        [Fact]
        public async Task Handle_QuandoNaoHouverCompras_DeveRetornarListaVazia()
        {
            // Arrange
            var usuarioId = 777;
            RepositoryMock.ConfigurarObterPorUsuario(new List<Domain.Compras.HistoricoCompra>());

            var query = new ConsultarHistoricoQuery(usuarioId);

            // Act
            var resultado = await Handler.Handle(query, CancellationToken.None);

            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Valor.Compras.Should().BeEmpty();

            RepositoryMock.GarantirObterPorUsuarioChamadoCom(usuarioId);
        }
    }
}
