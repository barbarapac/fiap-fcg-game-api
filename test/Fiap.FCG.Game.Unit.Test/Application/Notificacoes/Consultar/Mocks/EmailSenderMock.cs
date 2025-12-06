using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Notificacoes.Enviar;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Consultar.Mocks;

public class EmailSenderMock : Mock<IEmailSender>
{
    public void ConfigurarEnvioEmail()
    {
        Setup(s => s.EnviarAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
    }

    public void GarantirEnvioEmail(int vezes)
    {
        Verify(s => s.EnviarAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
            Times.Exactly(vezes));
    }
}