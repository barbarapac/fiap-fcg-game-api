using System.Linq;
using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Jogos.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Jogos.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Jogos
{
    public class JogoRepositoryTest : IClassFixture<JogoRepositoryFixture>
    {
        private readonly JogoRepositoryFixture _fixture;

        public JogoRepositoryTest(JogoRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ObterPorIdAsync_IdExistente_DeveRetornarJogo()
        {
            // Arrange
            var jogo = JogoFaker.Valido("The last of us");
            _fixture.Context.Add(jogo);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.ObterPorIdAsync(jogo.Id);

            // Assert
            resultado.Should().BeEquivalentTo(jogo);
        }

        [Fact]
        public async Task ObterPorNome_NomeExistente_DeveRetornarJogo()
        {
            // Arrange
            var jogo = JogoFaker.Valido("Elden Ring");
            _fixture.Context.Add(jogo);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.ObterPorNome(jogo.Nome);

            // Assert
            resultado.Should().BeEquivalentTo(jogo);
        }

        [Fact]
        public async Task ObterTodosAsync_QuandoChamado_DeveRetornarTodosOsJogos()
        {
            // Arrange
            var jogos = JogoFaker.ListaValida();
            _fixture.Context.AddRange(jogos);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.ObterTodosAsync();

            // Assert
            resultado.Should().NotBeNull();
        }

        [Fact]
        public async Task ObterPorIdsAsync_IdsValidos_DeveRetornarJogosEsperados()
        {
            // Arrange
            var jogos = JogoFaker.ListaValida();
            _fixture.Context.AddRange(jogos);
            await _fixture.Context.SaveChangesAsync();
            var ids = jogos.Select(j => j.Id).ToList();

            // Act
            var resultado = await _fixture.Repository.ObterPorIdsAsync(ids);

            // Assert
            resultado.Should().BeEquivalentTo(jogos);
        }

        [Fact]
        public async Task AdicionarAsync_JogoValido_DevePersistirEDevolverSucesso()
        {
            // Arrange
            var jogo = JogoFaker.Valido("Good");

            // Act
            var resultado = await _fixture.Repository.AdicionarAsync(jogo);

            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Valor.Should().BeEquivalentTo(jogo);
        }

        [Fact]
        public async Task AtualizarAsync_JogoValido_DeveAtualizarComSucesso()
        {
            // Arrange
            var jogo = JogoFaker.Valido("Ghost");
            _fixture.Context.Add(jogo);
            await _fixture.Context.SaveChangesAsync();

            var novoNome = "Jogo Atualizado";
            jogo.Atualizar(novoNome, 299);

            // Act
            await _fixture.Repository.AtualizarAsync(jogo);

            // Assert
            var atualizado = await _fixture.Repository.ObterPorIdAsync(jogo.Id);
            atualizado.Nome.Should().Be(novoNome);
        }

        [Fact]
        public async Task RemoverAsync_JogoExistente_DeveRemoverComSucesso()
        {
            // Arrange
            var jogo = JogoFaker.Valido("CS");
            _fixture.Context.Add(jogo);
            await _fixture.Context.SaveChangesAsync();

            // Act
            await _fixture.Repository.RemoverAsync(jogo);

            // Assert
            var resultado = await _fixture.Repository.ObterPorIdAsync(jogo.Id);
            resultado.Should().BeNull();
        }
    }
}