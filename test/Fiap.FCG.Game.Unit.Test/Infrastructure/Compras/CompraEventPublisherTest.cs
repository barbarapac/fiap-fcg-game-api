using Azure.Messaging.ServiceBus;
using Fiap.FCG.Game.Application.Eventos.ComprasEvent;
using Fiap.FCG.Game.Infrastructure.PublisherEvent.ComprasEvent;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras
{
    public class CompraEventPublisherTest
    {
        [Fact]
        public async Task PublicarCompraRealizadaAsync_DeveEnviarMensagemParaFila()
        {
            // Arrange
            var evento = new CompraRealizadaEvent
            {
                CompraId = 1,
                UsuarioId = 100,
                ValorTotal = 150,
                MetodoPagamento = EMetodoPagamento.Pix,
                DataCompra = DateTime.UtcNow
            };

            ServiceBusMessage capturedMessage = null;

            var senderMock = new Mock<ServiceBusSender>();
            senderMock
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default))
                .Callback<ServiceBusMessage, CancellationToken>((msg, _) => capturedMessage = msg)
                .Returns(Task.CompletedTask);

            var publisher = new CompraEventPublisher(senderMock.Object);

            // Act
            await publisher.PublicarCompraRealizadaAsync(evento);

            // Assert
            senderMock.Verify(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default), Times.Once);

            var deserialized = capturedMessage.Body.ToObjectFromJson<CompraRealizadaEvent>();
            deserialized.Should().NotBeNull();
            deserialized.CompraId.Should().Be(evento.CompraId);
            deserialized.UsuarioId.Should().Be(evento.UsuarioId);
            deserialized.ValorTotal.Should().Be(evento.ValorTotal);
        }

        [Fact]
        public void Constructor_ComIConfiguration_DeveCriarSenderComConnectionString()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["SERVICEBUS_CONNECTION"]).Returns("Endpoint=sb://test/;SharedAccessKeyName=name;SharedAccessKey=key");

            // Act
            var publisher = new CompraEventPublisher(configMock.Object);

            // Assert
            publisher.Should().NotBeNull();
        }

        [Fact]
        public async Task DisposeAsync_DeveChamarDisposeDeSenderEClient()
        {
            // Arrange
            var senderMock = new Mock<ServiceBusSender>();
            var clientMock = new Mock<ServiceBusClient>();

            senderMock.Setup(s => s.DisposeAsync()).Returns(ValueTask.CompletedTask).Verifiable();
            clientMock.Setup(c => c.DisposeAsync()).Returns(ValueTask.CompletedTask).Verifiable();

            var publisher = new CompraEventPublisherTestable(clientMock.Object, senderMock.Object);

            // Act
            await publisher.DisposeAsync();

            // Assert
            senderMock.Verify(s => s.DisposeAsync(), Times.Once);
            clientMock.Verify(c => c.DisposeAsync(), Times.Once);
        }

        private class CompraEventPublisherTestable : CompraEventPublisher
        {
            private readonly ServiceBusClient _clientInjected;
            private readonly ServiceBusSender _senderInjected;

            public CompraEventPublisherTestable(ServiceBusClient client, ServiceBusSender sender)
                : base(sender)
            {
                _clientInjected = client;
                _senderInjected = sender;

                typeof(CompraEventPublisher)
                    .GetField("_client", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(this, _clientInjected);
            }
        }
    }
}
