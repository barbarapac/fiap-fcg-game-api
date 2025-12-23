using Fiap.FCG.Game.Domain.Compras;
using Moq;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks
{
    public class BibliotecaRepositoryMock : Mock<IBibliotecaRepository>
    {
        public void ConfigurarUsuarioJaPossuiJogoAsync(bool resultado) =>
        Setup(x => x.UsuarioJaPossuiJogoAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(resultado);

        public void GarantirAdicionarAsync() =>
        Verify(x => x.AdicionarAsync(It.IsAny<BibliotecaJogo>()), Times.AtLeastOnce);

        public void ConfigurarObterPorUsuarioIdAsync(List<BibliotecaJogo> jogos) =>
        Setup(x => x.ObterPorUsuarioIdAsync(It.IsAny<int>())).ReturnsAsync(jogos);
    }
}
