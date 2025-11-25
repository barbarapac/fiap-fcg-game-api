using AutoBogus;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Unit.Test.Domin.Notificacoes.Fakers;

public static class JogoFaker
{
    public static Jogo Valido()
    {
        return new AutoFaker<Jogo>()
            .RuleFor(j => j.Nome, f => f.Commerce.ProductName())
            .Generate();
    }
}