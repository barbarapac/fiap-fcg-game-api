using Fiap.FCG.Game.Application.Jogos.Consultar;
using Fiap.FCG.Game.Unit.Test.Application.Jogos.Consultar.Mocks;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Consultar.Fixtures;

public abstract class ConsultaJogoQueryFixture
{
    protected JogoRepositoryMock JogoRepositoryMock { get; }
    protected Mock<IElasticConnector> ElasticConnectorMock { get; }
    protected ConsultaJogoQuery Consulta { get; }

    protected ConsultaJogoQueryFixture()
    {
        JogoRepositoryMock = new JogoRepositoryMock();
        ElasticConnectorMock = new Mock<IElasticConnector>();
        Consulta = new ConsultaJogoQuery(JogoRepositoryMock.Object, ElasticConnectorMock.Object);
    }
}