using System.Collections.Generic;
using AutoBogus;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Consultar.Fakers;

public static class JogoFaker
{
    public static Jogo Valido()
    {
        return new AutoFaker<Jogo>().Generate();
    }

    public static List<Jogo> ListaValida(int quantidade = 3)
    {
        return new AutoFaker<Jogo>().Generate(quantidade);
    }
}