using Fiap.FCG.Game.Domain.Compras;
using Moq;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Mocks
{
    public class BibliotecaRepositoryMock : Mock<IBibliotecaRepository>
    {
        public void ConfigurarObterPorUsuarioIdAsync(List<BibliotecaJogo> jogos) =>
        Setup(x => x.ObterPorUsuarioIdAsync(It.IsAny<int>())).ReturnsAsync(jogos);
    }
}
