using Fiap.FCG.Game.Domain._Shared;
using System;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Domain.Compras
{
    public class HistoricoCompra : Base
    {
        public int UsuarioId { get; private set; }
        public DateTime DataCompra { get; private set; } = DateTime.UtcNow;
        public List<ItemCompra> Itens { get; private set; } = new();

        private HistoricoCompra() { }

        public HistoricoCompra(int usuarioId, List<ItemCompra> itens)
        {
            UsuarioId = usuarioId;
            Itens = itens ?? throw new ArgumentNullException(nameof(itens));
        }
    }
}
