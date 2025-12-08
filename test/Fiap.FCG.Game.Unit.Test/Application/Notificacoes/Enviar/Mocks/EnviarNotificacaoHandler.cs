using Fiap.FCG.Game.Application.Notificacoes.Enviar;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Mocks;

public class EmailSenderMock : Mock<IEmailSender>
{
    public void GarantirEmailEnviado(string email)
    {
        Verify(x => x.EnviarAsync(email, It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
    }
}