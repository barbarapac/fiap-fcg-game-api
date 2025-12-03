using AutoBogus;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Fakers;

public static class JogoFaker
{
    public static Jogo Valido()
    {
        return new AutoFaker<Jogo>()
            .RuleFor(j => j.Id, f => f.Random.Int(1, 1000))
            .RuleFor(j => j.Nome, f => f.Commerce.ProductName())
            .RuleFor(j => j.Preco, f => f.Random.Decimal(10, 500))
            .Generate();
    }
}