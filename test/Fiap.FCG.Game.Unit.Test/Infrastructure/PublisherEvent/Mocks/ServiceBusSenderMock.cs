using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Mocks;

public class ServiceBusSenderMock : Mock<ServiceBusSender>
{
    private ServiceBusMessage? _ultimaMensagem;

    public ServiceBusSenderMock()
    {
        Setup(x => x.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default))
            .Callback<ServiceBusMessage, System.Threading.CancellationToken>((msg, _) => _ultimaMensagem = msg)
            .Returns(Task.CompletedTask);
    }

    public ServiceBusMessage ObterUltimaMensagemEnviada() => _ultimaMensagem!;

    public void GarantirMensagemEnviada()
    {
        Verify(x => x.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default), Times.Once);
    }
}