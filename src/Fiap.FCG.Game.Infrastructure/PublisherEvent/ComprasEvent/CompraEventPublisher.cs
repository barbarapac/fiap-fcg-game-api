using Azure.Messaging.ServiceBus;
using Fiap.FCG.Game.Application.Eventos.ComprasEvent;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Infrastructure.PublisherEvent.ComprasEvent
{
    public class CompraEventPublisher : ICompraEventPublisher, IAsyncDisposable
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public CompraEventPublisher(IConfiguration config)
        {
            var conn = Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION")
                       ?? config["SERVICEBUS_CONNECTION"];

            _client = new ServiceBusClient(conn);
            _sender = _client.CreateSender("compras-realizadas");
        }

        public CompraEventPublisher(ServiceBusSender sender)
        {
            _sender = sender;
        }

        public async Task PublicarCompraRealizadaAsync(CompraRealizadaEvent evento)
        {
            var json = JsonSerializer.Serialize(evento);
            var message = new ServiceBusMessage(json);
            await _sender.SendMessageAsync(message);
        }

        public async ValueTask DisposeAsync()
        {
            await _sender.DisposeAsync();
            await _client.DisposeAsync();
        }
    }
}
