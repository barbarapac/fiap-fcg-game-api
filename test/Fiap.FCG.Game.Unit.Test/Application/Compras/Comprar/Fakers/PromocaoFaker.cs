using AutoBogus;
using Fiap.FCG.Game.Domain.Promocoes;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Fakers
{
    public static class PromocaoFaker
    {
        public static List<PromocaoJogo> ListaComDesconto(int jogoId)
        {
            return new List<PromocaoJogo>
            {
                new AutoFaker<PromocaoJogo>()
                .RuleFor(p => p.JogoId, _ => jogoId)
                .RuleFor(p => p.Promocao, f => new AutoFaker<Promocao>().RuleFor(x => x.DescontoPercentual, _ => 10).Generate())
                .Generate()
            };
        }
    }
}
