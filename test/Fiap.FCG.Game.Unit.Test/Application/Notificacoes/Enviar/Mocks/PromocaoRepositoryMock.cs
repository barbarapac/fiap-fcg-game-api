using System.Collections.Generic;
using Fiap.FCG.Game.Domain.Promocoes;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Mocks;

public class PromocaoRepositoryMock : Mock<IPromocaoRepository>
{
    public void ConfigurarPromocoesAtivas(List<Promocao> promocoes)
    {
        Setup(x => x.ObterPromocoesAtivasComJogosAsync())
            .ReturnsAsync(promocoes);
    }

    public void GarantirObterPromocoesChamado()
    {
        Verify(x => x.ObterPromocoesAtivasComJogosAsync(), Times.Once);
    }
}