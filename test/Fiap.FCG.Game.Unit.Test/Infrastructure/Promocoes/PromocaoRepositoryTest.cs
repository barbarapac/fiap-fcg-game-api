using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes
{
    public class PromocaoRepositoryTests : IClassFixture<PromocaoRepositoryFixture>
    {
        private readonly PromocaoRepositoryFixture _fixture;

        public PromocaoRepositoryTests(PromocaoRepositoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.Reset();
        }

        [Fact]
        public async Task AdicionarAsync_DevePersistirPromocao()
        {
            // Arrange
            var promocao = PromocaoFaker.Valida();

            // Act
            var resultado = await _fixture.Repository.AdicionarAsync(promocao);

            // Assert
            resultado.Sucesso.Should().BeTrue();

            var armazenada = await _fixture.Context.Promocoes.FindAsync(promocao.Id);
            armazenada.Should().NotBeNull();
        }

        [Fact(DisplayName = "ExisteAsync deve retornar true quando nome já existe")]
        public async Task ExisteAsync_DeveRetornarTrue()
        {
            // Arrange
            var promocao = PromocaoFaker.Valida();
            _fixture.Context.Promocoes.Add(promocao);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var existe = await _fixture.Repository.ExisteAsync(promocao.Nome);

            // Assert
            existe.Should().BeTrue();
        }

        [Fact]
        public async Task ObterPromocoesAtivasComJogosAsync_DeveRetornarSomenteAtivas()
        {
            // Arrange
            var ativas = PromocaoFaker.ListaValida(2, true);
            var inativas = PromocaoFaker.ListaValida(2, false);

            _fixture.Context.AddRange(ativas);
            _fixture.Context.AddRange(inativas);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.ObterPromocoesAtivasComJogosAsync();

            // Assert
            resultado.Should().HaveCount(ativas.Count);
            resultado.Should().OnlyContain(p => p.DataFim >= System.DateTime.UtcNow);
        }

        [Fact]
        public async Task AtualizarAsync_DeveAtualizar()
        {
            // Arrange
            var promocao = PromocaoFaker.Valida();
            _fixture.Context.Add(promocao);
            await _fixture.Context.SaveChangesAsync();

            promocao.Atualizar("Novo Nome", "Alterada", 20, promocao.DataInicio, promocao.DataFim);

            // Act
            var resultado = await _fixture.Repository.AtualizarAsync(promocao);

            // Assert
            resultado.Sucesso.Should().BeTrue();
            (await _fixture.Context.Promocoes.FindAsync(promocao.Id))!.Nome.Should().Be("Novo Nome");
        }

        [Fact]
        public async Task ExcluirAsync_DeveRemover()
        {
            // Arrange
            var promocao = PromocaoFaker.Valida();
            _fixture.Context.Add(promocao);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.ExcluirAsync(promocao);

            // Assert
            resultado.Sucesso.Should().BeTrue();
            (await _fixture.Context.Promocoes.FindAsync(promocao.Id)).Should().BeNull();
        }
    }
}
