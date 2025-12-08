using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Domain.Jogos;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Domin.Compras
{
    public class ItemCompraTest
    {
        [Fact]
        public void Construtor_ComParametrosValidos_DeveInicializarPropriedadesCorretamente()
        {
            // Arrange
            var jogoId = 42;
            var precoPago = 59.99m;

            // Act
            var itemCompra = new ItemCompra(jogoId, precoPago);

            // Assert
            itemCompra.JogoId.Should().Be(jogoId);
            itemCompra.PrecoPago.Should().Be(precoPago);
        }

        [Fact]
        public void Propriedades_AposInstanciacao_DevePermitirAlteracaoDeHistoricoCompraId()
        {
            // Arrange
            var itemCompra = new ItemCompra(1, 10m);
            var historicoId = 99;

            // Act
            itemCompra.HistoricoCompraId = historicoId;

            // Assert
            itemCompra.HistoricoCompraId.Should().Be(historicoId);
        }

        [Fact]
        public void Propriedades_AposInstanciacao_DevePermitirAlteracaoDeHistoricoCompra()
        {
            // Arrange
            var itemCompra = new ItemCompra(1, 10m);
            var historico = new HistoricoCompra(1, [itemCompra]);

            // Act
            itemCompra.HistoricoCompra = historico;

            // Assert
            itemCompra.HistoricoCompra.Should().BeSameAs(historico);
        }

        [Fact]
        public void Propriedades_AposInstanciacao_DevePermitirAlteracaoDeJogo()
        {
            // Arrange
            var itemCompra = new ItemCompra(1, 10m);
            var jogo = new Jogo();

            // Act
            itemCompra.Jogo = jogo;

            // Assert
            itemCompra.Jogo.Should().BeSameAs(jogo);
        }
    }
}
