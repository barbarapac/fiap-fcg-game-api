using System;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Domain.Compras.Eventos
{
    public class CompraRealizadaEvent
    {
        public int UsuarioId { get; set; }        
        public DateTime DataCompra { get; set; }
        public List<CompraRealizadaItemEvent> Itens { get; set; } = new();
    }
}
