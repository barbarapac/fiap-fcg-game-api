using AutoBogus;
using Fiap.FCG.Game.Application.Promocoes.Remover;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Remover.Fakers;

public static class RemoverPromocaoCommandFaker
{
    public static RemoverPromocaoCommand ComIdValido()
    {
        return new AutoFaker<RemoverPromocaoCommand>()
            .RuleFor(x => x.PromocaoId, f => f.Random.Int(1, 10000))
            .Generate();
    }
}