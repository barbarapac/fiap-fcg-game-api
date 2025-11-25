using System.Collections.Generic;
using Fiap.FCG.Game.Domain.Promocoes;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Consultar.Mocks;

public class PromocaoRepositoryMock : Mock<IPromocaoRepository>
{
    public void ConfigurarPromocoes(List<Promocao> promocoes)
    {
        Setup(r => r.ObterPromocoesAtivasComJogosAsync())
            .ReturnsAsync(promocoes);
    }
}