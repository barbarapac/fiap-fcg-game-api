using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Fiap.FCG.Game.Application.Eventos.PromocaoEvent;
using Fiap.FCG.Game.Domain.Promocoes;
using Fiap.FCG.Game.Infrastructure.PublisherEvent._Shared;
using Microsoft.Extensions.Configuration;

namespace Fiap.FCG.Game.Infrastructure.PublisherEvent.PromocaoEvent;

public class PromocaoEventPublisher : IPromocaoEventPublisher
{
    private readonly ServiceBusClient _bus;
    private readonly ServiceBusSender _sender;

    public PromocaoEventPublisher(IConfiguration config)
    {
        var url = Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION") ?? config["SERVICEBUS_CONNECTION"];
        _bus = new ServiceBusClient(url);
        _sender = _bus.CreateSender("promocao-games");
    }

    public async Task PromocaoCadastradaPublishAsync(Promocao promocao)
    {
        var evento = new PromocaoEvent
        {
            Tipo       = TipoEvento.PROMOCAO_CADASTRADA,
            PromocaoId = promocao.Id,
            Titulo     = promocao.Nome,
            Desconto   = promocao.DescontoPercentual,
            DataInicio = promocao.DataInicio,
            DataFim    = promocao.DataFim,
            Jogos      = promocao.Jogos.Select(j => new JogoEventoDto
            {
                JogoId = j.JogoId,
                Nome = j.Jogo?.Nome ?? "N/A",
                Preco = j.Jogo?.Preco ?? 0
            }).ToList()
        };

        await PublicarAsync(evento);
    }

    public async Task PromocaEditadaPublishAsync(Promocao promocao)
    {
        var evento = new PromocaoEvent
        {
            Tipo       = TipoEvento.PROMOCAO_ATUALIZADA,
            PromocaoId = promocao.Id,
            Titulo     = promocao.Nome,
            Desconto   = promocao.DescontoPercentual,
            DataInicio = promocao.DataInicio,
            DataFim    = promocao.DataFim,
            Jogos      = promocao.Jogos.Select(j => new JogoEventoDto
            {
                JogoId = j.JogoId,
                Nome = j.Jogo?.Nome ?? "N/A",
                Preco = j.Jogo?.Preco ?? 0
            }).ToList()
        };

        await PublicarAsync(evento);
    }

    public async Task PromocaoRemovidaPublishAsync(Promocao promocao)
    {
        var evento = new PromocaoEvent
        {
            Tipo       = TipoEvento.PROMOCAO_DELETADA,
            PromocaoId = promocao.Id,
            Titulo     = promocao.Nome,
            Desconto   = promocao.DescontoPercentual,
            DataInicio = promocao.DataInicio,
            DataFim    = promocao.DataFim,
            Jogos      = promocao.Jogos.Select(j => new JogoEventoDto
            {
                JogoId = j.JogoId,
                Nome = j.Jogo?.Nome ?? "N/A",
                Preco = j.Jogo?.Preco ?? 0
            }).ToList()
        };

        await PublicarAsync(evento);
    }
    
    private async Task PublicarAsync(PromocaoEvent evento)
    {
        var json = JsonSerializer.Serialize(evento);
        await _sender.SendMessageAsync(new ServiceBusMessage(json));
    }
}
