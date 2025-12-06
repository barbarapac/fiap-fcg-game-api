using Fiap.FCG.Game.Domain.Promocoes;
using Moq;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks
{
    public class PromocaoRepositoryMock : Mock<IPromocaoRepository>
    {
        public void ConfigurarObterPorJogosIdsAsync(List<PromocaoJogo> promocoes) =>
        Setup(x => x.ObterPorJogosIdsAsync(It.IsAny<List<int>>())).ReturnsAsync(promocoes);


        public void ConfigurarObterPorJogosIdsAsyncVazio() =>
        Setup(x => x.ObterPorJogosIdsAsync(It.IsAny<List<int>>())).ReturnsAsync(new List<PromocaoJogo>());
    }
}
