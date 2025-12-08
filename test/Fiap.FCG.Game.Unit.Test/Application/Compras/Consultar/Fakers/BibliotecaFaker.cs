using AutoBogus;
using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Domain.Jogos;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fakers
{
    public static class BibliotecaFaker
    {
        public static List<BibliotecaJogo> ListaComJogos()
        {
            return new List<BibliotecaJogo>
            {
                new AutoFaker<BibliotecaJogo>()
                .RuleFor(b => b.Jogo, _ => new AutoFaker<Jogo>().RuleFor(j => j.Nome, f => f.Commerce.ProductName()).Generate())
                .Generate(),
                new AutoFaker<BibliotecaJogo>()
                .RuleFor(b => b.Jogo, _ => new AutoFaker<Jogo>().RuleFor(j => j.Nome, f => f.Commerce.ProductName()).Generate())
                .Generate()
            };
        }
    }
}
