using Fiap.FCG.Game.Application.Promocoes.Cadastar;
using Fiap.FCG.Game.Unit.Test.Application.Promocoes.Cadastrar.Mocks;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Cadastrar.Fixtures;

public class CadastrarPromocaoHandlerFixture
{
    protected JogoRepositoryMock JogoRepositoryMock { get; private set; }
    protected PromocaoRepositoryMock PromocaoRepositoryMock { get; private set; }
    protected CadastrarPromocaoHandler Handler { get; private set; }

    public CadastrarPromocaoHandlerFixture()
    {
        JogoRepositoryMock = new JogoRepositoryMock();
        PromocaoRepositoryMock = new PromocaoRepositoryMock();

        Handler = new CadastrarPromocaoHandler(
            JogoRepositoryMock.Object,
            PromocaoRepositoryMock.Object
        );
    }
}