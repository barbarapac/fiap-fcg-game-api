using Fiap.FCG.Game.Application.Promocoes.Consultar;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Consultar.Fixtures;

public abstract class ConsultaPromocaoQueryFixture
{
    protected PromocaoRepositoryMock PromocaoRepositoryMock { get; }
    protected ConsultaPromocaoQuery ConsultaQuery { get; }

    protected ConsultaPromocaoQueryFixture()
    {
        PromocaoRepositoryMock = new PromocaoRepositoryMock();
        ConsultaQuery = new ConsultaPromocaoQuery(PromocaoRepositoryMock.Object);
    }
}