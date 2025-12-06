using System;
using System.Collections.Generic;
using Fiap.FCG.Game.Infrastructure.PublisherEvent._Shared;

namespace Fiap.FCG.Game.Infrastructure.PublisherEvent.PromocaoEvent;

public class PromocaoEvent
{
    public int PromocaoId { get; set; }
    public decimal Desconto { get; set; }
    public TipoEvento Tipo { get; set; }
    public List<JogoEventoDto> Jogos { get; set; } = new();
    public string Titulo { get; set; }
    public DateTime? DataFim { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime DataEvento { get; set; } = DateTime.UtcNow;
}

public class JogoEventoDto
{
    public int JogoId { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}
