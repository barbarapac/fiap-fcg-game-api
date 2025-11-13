using Fiap.FCG.Game.Application.Jogos.Consultar;
using Fiap.FCG.Game.Unit.Test.Application.Jogos.Consultar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Consultar.Fixtures;

public abstract class ConsultaJogoQueryFixture
{
    protected JogoRepositoryMock JogoRepositoryMock { get; }
    protected ConsultaJogoQuery Consulta { get; }

    protected ConsultaJogoQueryFixture()
    {
        JogoRepositoryMock = new JogoRepositoryMock();
        Consulta = new ConsultaJogoQuery(JogoRepositoryMock.Object);
    }
}