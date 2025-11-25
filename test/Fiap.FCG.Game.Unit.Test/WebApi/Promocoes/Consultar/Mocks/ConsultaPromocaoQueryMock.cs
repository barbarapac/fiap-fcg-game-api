using System.Collections.Generic;
using Fiap.FCG.Game.Application.Promocoes.Consultar;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Consultar.Mocks;

public class ConsultaPromocaoQueryMock : Mock<IConsultaPromocaoQuery>
{
    public void ConfigurarObterTodasAsync(List<PromocaoResponse> resultado)
    {
        Setup(x => x.ObterTodasAsync()).ReturnsAsync(resultado);
    }

    public void ConfigurarObterPorIdAsync(PromocaoResponse? resultado)
    {
        Setup(x => x.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(resultado);
    }
}