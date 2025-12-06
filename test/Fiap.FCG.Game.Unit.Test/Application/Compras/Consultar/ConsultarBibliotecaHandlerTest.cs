using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fakers;
using Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fixtures;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar
{
    public class ConsultarBibliotecaHandlerTest : ConsultarBibliotecaHandlerFixture
    {
        [Fact]
        public async Task Handle_QuandoUsuarioPossuiJogos_DeveRetornarListaComJogos()
        {
            // Arrange
            var query = ConsultarBibliotecaQueryFaker.ComValido();
            var biblioteca = BibliotecaFaker.ListaComJogos();


            BibliotecaRepositoryMock.ConfigurarObterPorUsuarioIdAsync(biblioteca);


            // Act
            var result = await Handler.Handle(query, default);


            // Assert
            result.Sucesso.Should().BeTrue();
            result.Valor.Should().HaveCount(biblioteca.Count);
            result.Valor.All(r => !string.IsNullOrWhiteSpace(r.Nome)).Should().BeTrue();
        }


        [Fact]
        public async Task Handle_QuandoUsuarioNaoPossuiJogos_DeveRetornarListaVazia()
        {
            // Arrange
            var query = ConsultarBibliotecaQueryFaker.ComValido();
            BibliotecaRepositoryMock.ConfigurarObterPorUsuarioIdAsync(new List<BibliotecaJogo>());


            // Act
            var result = await Handler.Handle(query, default);


            // Assert
            result.Sucesso.Should().BeTrue();
            result.Valor.Should().BeEmpty();
        }
    }
}
