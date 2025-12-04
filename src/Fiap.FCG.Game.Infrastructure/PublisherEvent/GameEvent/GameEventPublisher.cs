using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Fiap.FCG.Game.Application.Eventos.GameEvent;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Infrastructure.PublisherEvent._Shared;
using Microsoft.Extensions.Configuration;

namespace Fiap.FCG.Game.Infrastructure.PublisherEvent.GameEvent;

public class GameEventPublisher : IGameEventPublisher
{
    private readonly ServiceBusClient _bus;
    private readonly ServiceBusSender _sender;

    public GameEventPublisher(IConfiguration config)
    {
        var url = Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION") ?? config["SERVICEBUS_CONNECTION"];
        _bus = new ServiceBusClient(url);
        _sender = _bus.CreateSender("events-games");
    }

    public async Task JogoCadastradoPublishAsync(Jogo jogo)
    {
        var evento = new GameEvent()
        {
            Tipo = TipoEvento.JOGO_CADASTADO,
            JogoId = jogo.Id,
            Nome = jogo.Nome,
            Preco = jogo.Preco,
            DataEvento = DateTime.Now
        };

        await PublicarAsync(evento);
    }

    public async Task JogoEditadoPublishAsync(Jogo jogo)
    {
        var evento = new GameEvent()
        {
            Tipo = TipoEvento.JOGO_ATUALIZADO,
            JogoId = jogo.Id,
            Nome = jogo.Nome,
            Preco = jogo.Preco,
            DataEvento = DateTime.Now
        };

        await PublicarAsync(evento);
    }

    private async Task PublicarAsync(GameEvent evento)
    {
        var json = JsonSerializer.Serialize(evento);
        await _sender.SendMessageAsync(new ServiceBusMessage(json));
    }
}
