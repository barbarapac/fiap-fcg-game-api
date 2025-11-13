using System.Collections.Generic;
using Fiap.FCG.Game.Application.Jogos.Consultar;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Mocks;

public class ConsultaJogoQueryMock : Mock<IConsultaJogoQuery>
{
    public void ConfigurarObterPorId(int id, JogoResponse response)
    {
        Setup(c => c.ObterPorIdAsync(id))
            .ReturnsAsync(response);
    }

    public void ConfigurarObterTodos(List<JogoResponse> jogos)
    {
        Setup(c => c.ObterTodosAsync())
            .ReturnsAsync(jogos);
    }

    public void GarantirObterPorIdChamado()
    {
        Verify(c => c.ObterPorIdAsync(It.IsAny<int>()), Times.Once);
    }

    public void GarantirObterTodosChamado()
    {
        Verify(c => c.ObterTodosAsync(), Times.Once);
    }
}