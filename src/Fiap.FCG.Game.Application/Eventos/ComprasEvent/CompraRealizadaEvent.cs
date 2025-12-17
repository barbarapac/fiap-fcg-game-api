using System;

namespace Fiap.FCG.Game.Application.Eventos.ComprasEvent
{
    public class CompraRealizadaEvent
    {
        public int CompraId { get; set; }
        public int UsuarioId { get; set; }
        public decimal ValorTotal { get; set; }
        public EMetodoPagamento MetodoPagamento { get; set; }
        public EBandeiraCartao? BandeiraCartao { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
