using AutoBogus;
using Fiap.FCG.Game.Domain.Jogos;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Fakers
{
    public static class JogoFaker
    {
        public static List<Jogo> ListaComUm() => new List<Jogo> { new AutoFaker<Jogo>().Generate() };
    }
}
