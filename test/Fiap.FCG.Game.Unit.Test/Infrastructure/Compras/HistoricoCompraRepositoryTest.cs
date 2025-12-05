using System;
using System.Linq;
using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras
{
    public class HistoricoCompraRepositoryTests : IDisposable
    {
        private readonly HistoricoCompraRepositoryFixture _fixture;

        public HistoricoCompraRepositoryTests()
        {
            _fixture = new HistoricoCompraRepositoryFixture();
        }

        public void Dispose()
        {
            _fixture.Context.Database.EnsureDeleted();
            _fixture.Context.Dispose();
        }

        [Fact]
        public async Task ObterHistoricoAsync_DeveRetornarApenasDoUsuario()
        {
            // Arrange
            var usuarioId = 30;
            var comprasUsuario = HistoricoCompraFaker.ListaValida(3, usuarioId);
            var comprasOutros = HistoricoCompraFaker.ListaValida(2);

            _fixture.Context.AddRange(comprasUsuario);
            _fixture.Context.AddRange(comprasOutros);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.ObterHistoricoAsync(usuarioId);

            // Assert
            resultado.Should().HaveCount(comprasUsuario.Count);
            resultado.Should().OnlyContain(x => x.UsuarioId == usuarioId);
        }

        //[Fact]
        //public async Task ObterHistoricoAsync_DeveRetornarOrdenado()
        //{
        //    // Arrange
        //    var usuarioId = 42;

        //    var comprasAntigas = HistoricoCompraFaker.ListaValida(2, usuarioId)
        //        .Select(c => { c.DataCompra = c.DataCompra.AddDays(-10); return c; })
        //        .ToList();

        //    var comprasRecentes = HistoricoCompraFaker.ListaValida(2, usuarioId)
        //        .Select(c => { c.DataCompra = c.DataCompra.AddDays(1); return c; })
        //        .ToList();

        //    _fixture.Context.AddRange(comprasAntigas);
        //    _fixture.Context.AddRange(comprasRecentes);
        //    await _fixture.Context.SaveChangesAsync();

        //    // Act
        //    var resultado = await _fixture.Repository.ObterHistoricoAsync(usuarioId);

        //    // Assert
        //    resultado.Should().BeInDescendingOrder(x => x.DataCompra);
        //}

        [Fact]
        public async Task ObterHistoricoAsync_DeveRetornarListaVazia()
        {
            // Arrange
            var usuarioId = 999;

            // Act
            var resultado = await _fixture.Repository.ObterHistoricoAsync(usuarioId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeEmpty();
        }
    }
}
