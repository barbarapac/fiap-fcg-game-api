using Azure.Messaging.ServiceBus;
using Fiap.FCG.Game.Application.Eventos.ComprasEvent;
using Fiap.FCG.Game.Infrastructure.PublisherEvent.ComprasEvent;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
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
            senderMock.Verify(
                s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default),
                Times.Once);

            // Use ToObjectFromJson to safely deserialize BinaryData
            var deserialized = capturedMessage.Body.ToObjectFromJson<CompraRealizadaEvent>();

            deserialized.Should().NotBeNull();
            deserialized!.CompraId.Should().Be(evento.CompraId);
            deserialized.UsuarioId.Should().Be(evento.UsuarioId);
            deserialized.ValorTotal.Should().Be(evento.ValorTotal);

        }

        // OBS: CompraEventPublisherTestable não é mais necessário com o construtor público que aceita o sender mock.
    }
}
