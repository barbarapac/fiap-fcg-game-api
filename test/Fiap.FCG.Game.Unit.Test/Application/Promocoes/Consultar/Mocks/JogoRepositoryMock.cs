using System.Collections.Generic;
using Fiap.FCG.Game.Domain.Jogos;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Consultar.Mocks;

public class JogoRepositoryMock : Mock<IJogoRepository>
{
    public void ConfigurarObterAsync(List<int> ids, List<Jogo> jogos)
    {
        Setup(j => j.ObterPorIdsAsync(ids)).ReturnsAsync(jogos);
    }
}