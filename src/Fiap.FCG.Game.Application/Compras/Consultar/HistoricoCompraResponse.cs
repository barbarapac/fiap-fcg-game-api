using System;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public class HistoricoCompraResponse
    {
        public List<CompraDto> Compras { get; set; } = [];        

        public class CompraDto
        {
            public DateTime DataCompra { get; set; }
            public string NomeJogo { get; set; }
            public decimal ValorBase { get; set; }
            public decimal ValorComDesconto { get; set; }
        }
    }
}
