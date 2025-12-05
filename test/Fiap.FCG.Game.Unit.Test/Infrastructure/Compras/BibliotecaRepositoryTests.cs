using System.Threading.Tasks;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras
{
    public class BibliotecaRepositoryTests : IClassFixture<BibliotecaRepositoryFixture>
    {
        private readonly BibliotecaRepositoryFixture _fixture;

        public BibliotecaRepositoryTests(BibliotecaRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AdicionarAsync_DevePersistirBibliotecaDoUsuario()
        {
            // Arrange
            var jogo = BibliotecaJogoFaker.Valido();

            // Act
            await _fixture.Repository.AdicionarAsync(jogo);

            // Assert
            var encontrado = await _fixture.Context.BibliotecaJogos.FindAsync(jogo.Id);
            encontrado.Should().NotBeNull();
            encontrado!.UsuarioId.Should().Be(jogo.UsuarioId);
            encontrado.JogoId.Should().Be(jogo.JogoId);
        }

        [Fact]
        public async Task ObterBibliotecaAsync_DeveRetornarSomenteDoUsuario()
        {
            // Arrange
            var usuarioId = 50;
            var jogosUsuario = BibliotecaJogoFaker.ListaValida(3, usuarioId);
            var jogosOutrosUsuarios = BibliotecaJogoFaker.ListaValida(2);

            _fixture.Context.AddRange(jogosUsuario);
            _fixture.Context.AddRange(jogosOutrosUsuarios);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.ObterPorUsuarioIdAsync(usuarioId);

            // Assert            
            resultado.Should().OnlyContain(j => j.UsuarioId == usuarioId);
        }

        [Fact]
        public async Task UsuarioJaPossuiJogoAsync_DeveRetornarTrue()
        {
            // Arrange
            var jogo = BibliotecaJogoFaker.Valido();
            _fixture.Context.Add(jogo);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.UsuarioJaPossuiJogoAsync(jogo.UsuarioId, jogo.JogoId);

            // Assert
            resultado.Should().BeTrue();
        }

        [Fact]
        public async Task UsuarioJaPossuiJogoAsync_DeveRetornarFalse()
        {
            // Arrange
            var jogo = BibliotecaJogoFaker.Valido();
            _fixture.Context.Add(jogo);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var resultado = await _fixture.Repository.UsuarioJaPossuiJogoAsync(jogo.UsuarioId, 99999);

            // Assert
            resultado.Should().BeFalse();
        }
    }
}
