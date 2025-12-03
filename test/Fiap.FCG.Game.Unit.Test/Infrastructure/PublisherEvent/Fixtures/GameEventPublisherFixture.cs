using Fiap.FCG.Game.Infrastructure.PublisherEvent.GameEvent;
using Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Mocks;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Fixtures;

public class GameEventPublisherFixture
{
    protected internal ServiceBusSenderMock ServiceBusSenderMock { get; }
    protected internal GameEventPublisher Publisher { get; }

    public GameEventPublisherFixture()
    {
        // Mock do ServiceBusSender
        ServiceBusSenderMock = new ServiceBusSenderMock();

        // Mock da IConfiguration para evitar uso de string real
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["SERVICEBUS_CONNECTION"])
            .Returns("Endpoint=sb://fake.servicebus.windows.net/;SharedAccessKeyName=fake;SharedAccessKey=fakekey");

        // Instância real do Publisher (sem falhar na connection string)
        Publisher = new GameEventPublisher(configMock.Object);

        // Usando reflection para substituir o campo privado _sender
        typeof(GameEventPublisher)
            .GetField("_sender", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .SetValue(Publisher, ServiceBusSenderMock.Object);
    }
}