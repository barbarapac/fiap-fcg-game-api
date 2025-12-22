using Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Fakers;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Tests;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar
{
    public class ComprarJogoCommandHandlerTest : ComprarJogoCommandHandlerFixture
    {
        [Fact]
        public async Task Handle_QuandoJogoNaoEncontrado_DeveRetornarFalha()
        {
            // Arrange
            var command = ComprarJogoCommandFaker.ComValido();
            JogoRepositoryMock.ConfigurarObterPorIdsAsyncParaRetornarVazio();


            // Act
            var result = await Handler.Handle(command, default);


            // Assert
            result.Erro.Should().Be("Um ou mais jogos não foram encontrados.");
        }


        [Fact]
        public async Task Handle_QuandoUsuarioJaPossuiJogo_DeveRetornarFalha()
        {
            // Arrange
            var jogos = JogoFaker.ListaComUm();
            var command = ComprarJogoCommandFaker.ComIdsPersonalizados(jogos.Select(j => j.Id).ToList());


            JogoRepositoryMock.ConfigurarObterPorIdsAsync(jogos);
            PromocaoRepositoryMock.ConfigurarObterPorJogosIdsAsyncVazio();
            BibliotecaRepositoryMock.ConfigurarUsuarioJaPossuiJogoAsync(true);


            // Act
            var result = await Handler.Handle(command, default);


            // Assert
            result.Erro.Should().Contain("O usuário já possui o jogo");
        }


        [Fact]
        public async Task Handle_QuandoDadosValidos_DeveSalvarCompraEBibliotecaEPublicarEvento()
        {
            // Arrange
            var jogos = JogoFaker.ListaComUm();
            var command = ComprarJogoCommandFaker.ComIdsPersonalizados(jogos.Select(j => j.Id).ToList());
            var promocoes = PromocaoFaker.ListaComDesconto(jogos[0].Id);

            JogoRepositoryMock.ConfigurarObterPorIdsAsync(jogos);
            PromocaoRepositoryMock.ConfigurarObterPorJogosIdsAsync(promocoes);
            BibliotecaRepositoryMock.ConfigurarUsuarioJaPossuiJogoAsync(false);
            CompraRepositoryMock.ConfigurarAdicionarAsyncComId(123);
            CompraEventPublisherMock.ConfigurarPublicarCompraRealizadaAsync();

            var precoOriginal = jogos[0].Preco;
            var desconto = promocoes[0].Promocao.DescontoPercentual;
            var valorFinal = precoOriginal - (precoOriginal * desconto / 100);

            // Act
            var result = await Handler.Handle(command, default);

            // Assert
            result.Sucesso.Should().BeTrue();
            CompraRepositoryMock.GarantirAdicionarAsync();
            BibliotecaRepositoryMock.GarantirAdicionarAsync();
            CompraEventPublisherMock.GarantirPublicarCompraRealizadaAsync(123, command.UsuarioId, valorFinal);
        }

    }
}
