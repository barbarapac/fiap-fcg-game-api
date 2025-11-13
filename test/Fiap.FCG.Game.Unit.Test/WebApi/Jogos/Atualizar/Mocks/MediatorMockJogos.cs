using Fiap.FCG.Game.Application.Jogos.Atualizar;
using Fiap.FCG.Game.Domain._Shared;
using MediatR;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Atualizar.Mocks;

public class MediatorMockJogos : Mock<IMediator>
{
    public void ConfigurarEnvio(AtualizarJogoCommand comando, Result<bool> resultado)
    {
        Setup(m => m.Send(comando, default)).ReturnsAsync(resultado);
    }

    public void GarantirEnvio(AtualizarJogoCommand comando)
    {
        Verify(m => m.Send(comando, default), Times.Once);
    }
    
}
