using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras
{
    public class CompraRepositoryTests : IClassFixture<CompraRepositoryFixture>
    {
        private readonly CompraRepositoryFixture _fixture;

        public CompraRepositoryTests(CompraRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AdicionarAsync_DevePersistirCompra()
        {
            // Arrange
            var compra = HistoricoCompraFaker.Valido();

            // Act
            await _fixture.Repository.AdicionarAsync(compra);

            // Assert
            var encontrado = await _fixture.Context.HistoricoCompras.FindAsync(compra.Id);
            encontrado.Should().NotBeNull();
            encontrado!.UsuarioId.Should().Be(compra.UsuarioId);
        }

        //[Fact]
        //public async Task ObterPorUsuarioAsync_DeveRetornarOrdenado()
        //{
        //    // Arrange
        //    var usuarioId = 42;

        //    var antigas = HistoricoCompraFaker.ListaValida(2, usuarioId);
        //    var recentes = HistoricoCompraFaker.ListaValida(2, usuarioId);

        //    antigas.ForEach(c => c.DataCompra = c.DataCompra.AddDays(-10));
        //    recentes.ForEach(c => c.DataCompra = c.DataCompra.AddDays(1));

        //    _fixture.Context.AddRange(antigas);
        //    _fixture.Context.AddRange(recentes);
        //    await _fixture.Context.SaveChangesAsync();

        //    // Act
        //    var resultado = await _fixture.Repository.ObterPorUsuarioAsync(usuarioId);

        //    // Assert
        //    resultado.Should().HaveCount(4);
        //    resultado.Should().BeInDescendingOrder(c => c.DataCompra);
        //}

        [Fact]
        public async Task ObterPorUsuarioAsync_DeveRetornarSomenteDoUsuario()
        {
            // Arrange
            var usuarioId = 99;
            var comprasUsuario = HistoricoCompraFaker.ListaValida(3, usuarioId);
            var comprasOutros = HistoricoCompraFaker.ListaValida(2);

            _fixture.Context.AddRange(comprasUsuario);
            _fixture.Context.AddRange(comprasOutros);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.ObterPorUsuarioAsync(usuarioId);

            // Assert
            resultado.Should().OnlyContain(c => c.UsuarioId == usuarioId);
        }
    }
}
