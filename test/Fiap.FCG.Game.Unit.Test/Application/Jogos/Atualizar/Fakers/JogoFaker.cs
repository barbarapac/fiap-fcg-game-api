using AutoBogus;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Atualizar.Fakers;

public static class JogoFaker
{
    public static Jogo Valido()
    {
        return new AutoFaker<Jogo>().Generate();
    }
}