using Fiap.FCG.Game.Infrastructure._Shared;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Mocks;

public class EmailSenderMock : Mock<IEmailSender>
{
    public void GarantirEmailEnviado(string email)
    {
        Verify(x => x.EnviarAsync(email, It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
    }
}