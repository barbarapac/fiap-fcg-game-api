using Fiap.FCG.Game.Infrastructure.PublisherEvent.PromocaoEvent;
using Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Mocks;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Fixtures;

public class PromocaoEventPublisherFixture
{
    protected internal ServiceBusSenderMock ServiceBusSenderMock { get; }
    protected internal PromocaoEventPublisher Publisher { get; }


    public PromocaoEventPublisherFixture()
    {
        ServiceBusSenderMock = new ServiceBusSenderMock();


        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["SERVICEBUS_CONNECTION"]) // evita crash do construtor real
            .Returns("Endpoint=sb://fake.servicebus.windows.net/;SharedAccessKeyName=fake;SharedAccessKey=fakekey");


        Publisher = new PromocaoEventPublisher(configMock.Object);


        typeof(PromocaoEventPublisher)
            .GetField("_sender", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .SetValue(Publisher, ServiceBusSenderMock.Object);
    }
}