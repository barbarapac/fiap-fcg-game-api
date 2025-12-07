using AutoBogus;
using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Domain.Jogos;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fakers
{
    public static class HistoricoCompraFaker
    {
        public static HistoricoCompra ValidoComJogos(int usuarioId, int quantidade = 1)
        {
            var jogos = new AutoFaker<Jogo>()
                .RuleFor(j => j.Nome, f => f.Commerce.ProductName())
                .RuleFor(j => j.Preco, f => f.Random.Decimal(20, 200))
                .Generate(quantidade);

            var itens = new List<ItemCompra>();
            foreach (var jogo in jogos)
            {
                itens.Add(new ItemCompra(jogo.Id, jogo.Preco * 0.9m) { Jogo = jogo });
            }

            return new HistoricoCompra(usuarioId, itens);
        }
    }
}
