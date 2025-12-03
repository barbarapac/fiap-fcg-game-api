using System.Threading.Tasks;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Unit.Test.Application.Jogos.Cadastrar.Fakers;
using Fiap.FCG.Game.Unit.Test.Application.Jogos.Cadastrar.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Cadastrar
{
    public class CadastrarJogoHandlerTest : CadastrarJogoHandlerFixture
    {
        [Fact]
        public async Task Handle_QuandoEmailJaCadastrado_DeveRetornarFalha()
        {
            // Arrange
            var command = CadastrarJogoCommandFaker.Valido();
            JogoRepositoryMock.ConfigurarParaRetornarJogoAoObterPorNome(JogoFaker.ConverterParaJogo(command));

            // Act
            var resultado = await Handler.Handle(command, default);

            // Assert
            resultado.Sucesso.Should().BeFalse();
            resultado.Erro.Should().Be("Jogo já cadastrado.");
            JogoRepositoryMock.GarantirQueNaoChamouAdicionar();
            GameEventPublisherMock.GarantirJogoCadastradoPublishAsyncNaoChamado();
        }

        [Fact]
        public async Task Handle_QuandoUsuarioEhValido_DeveAdicionarEPublicarEvento()
        {
            // Arrange
            var command = CadastrarJogoCommandFaker.Valido();

            JogoRepositoryMock.ConfigurarParaRetornarJogoAoObterPorNome(null);

            var jogo = Jogo.Criar(command.Nome, command.Preco).Valor;

            JogoRepositoryMock.ConfigurarParaRetornarJogoAoAdicionar(Result.Success(jogo));
            GameEventPublisherMock.ConfigurarJogoCadastradoPublishAsync();

            // Act
            var resultado = await Handler.Handle(command, default);

            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Valor.Should().Be(jogo.Id.ToString());

            JogoRepositoryMock.GarantirChamadaAdicionar();
            GameEventPublisherMock.GarantirJogoCadastradoPublishAsyncChamado(jogo);
        }

        [Fact]
        public async Task Handle_QuandoJogoInvalido_DeveRetornarErro()
        {
            // Arrange
            var command = CadastrarJogoCommandFaker.ComNomeInvalido();
            JogoRepositoryMock.ConfigurarParaRetornarJogoAoObterPorNome(null);

            // Act
            var resultado = await Handler.Handle(command, default);

            // Assert
            resultado.Sucesso.Should().BeFalse();
            resultado.Erro.Should().Be("Nome é obrigatório.");
            JogoRepositoryMock.GarantirQueNaoChamouAdicionar();
            GameEventPublisherMock.GarantirJogoCadastradoPublishAsyncNaoChamado();
        }

        [Fact]
        public async Task Handle_QuandoAdicionarRetornaErro_NaoDevePublicarEvento()
        {
            // Arrange
            var command = CadastrarJogoCommandFaker.Valido();
            JogoRepositoryMock.ConfigurarParaRetornarJogoAoObterPorNome(null);

            var jogo = Jogo.Criar(command.Nome, command.Preco).Valor;
            JogoRepositoryMock.ConfigurarParaRetornarJogoAoAdicionar(Result.Failure<Jogo>("Erro ao adicionar"));

            // Act
            var resultado = await Handler.Handle(command, default);

            // Assert
            resultado.Sucesso.Should().BeFalse();
            resultado.Erro.Should().Be("Erro ao adicionar");
            GameEventPublisherMock.GarantirJogoCadastradoPublishAsyncNaoChamado();
        }
    }
}
