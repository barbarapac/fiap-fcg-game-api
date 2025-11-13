using Fiap.FCG.Game.Application.Jogos.Cadastrar;
using Fiap.FCG.Game.Domain._Shared;
using MediatR;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Cadastrar.Mocks;

public class MediatorMockJogos : Mock<IMediator>
{
    public void ConfigurarEnvio(CadastrarJogoCommand comando, Result<string> resultado)
    {
        Setup(m => m.Send(comando, default)).ReturnsAsync(resultado);
    }

    public void GarantirEnvio(CadastrarJogoCommand comando)
    {
        Verify(m => m.Send(comando, default), Times.Once);
    }
}
