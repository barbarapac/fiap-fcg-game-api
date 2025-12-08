using Fiap.FCG.Game.Application.Compras.Consultar;
using System.Linq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fakers
{
    public static class HistoricoCompraResponseFaker
    {
        public static HistoricoCompraResponse ComCompras(int usuarioId, int quantidade = 1)
        {
            var historicos = HistoricoCompraFaker.GerarLista(quantidade, usuarioId);

            var compras = historicos
                .SelectMany(c => c.Itens
                    .Where(i => i.Jogo != null)
                    .Select(i => new HistoricoCompraResponse.CompraDto
                    {
                        DataCompra = c.DataCompra,
                        NomeJogo = i.Jogo.Nome ?? "Desconhecido",
                        ValorBase = i.Jogo.Preco,
                        ValorComDesconto = i.PrecoPago
                    }))
                .ToList();

            return new HistoricoCompraResponse
            {
                Compras = compras
            };
        }
    }
}
