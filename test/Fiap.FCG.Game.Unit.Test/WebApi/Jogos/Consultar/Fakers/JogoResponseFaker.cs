using System.Collections.Generic;
using System.Linq;
using AutoBogus;
using Fiap.FCG.Game.Application.Jogos.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Fakers;

public static class JogoResponseFaker
{
    public static JogoResponse Valido()
    {
        return new AutoFaker<JogoResponse>()
            .RuleFor(j => j.Nome, f => f.Commerce.ProductName())
            .RuleFor(j => j.Preco, f => f.Random.Decimal(10, 500))
            .Generate();
    }

    public static List<JogoResponse> Lista(int quantidade = 3)
    {
        return Enumerable.Range(1, quantidade)
            .Select(_ => Valido())
            .ToList();
    }
}