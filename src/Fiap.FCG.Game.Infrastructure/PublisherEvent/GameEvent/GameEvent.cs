using System;
using Fiap.FCG.Game.Infrastructure.PublisherEvent._Shared;

namespace Fiap.FCG.Game.Infrastructure.PublisherEvent.GameEvent;

public class GameEvent
{
    public TipoEvento Tipo { get; set; }
    public int JogoId { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public DateTime DataEvento { get; set; } = DateTime.UtcNow;
}
