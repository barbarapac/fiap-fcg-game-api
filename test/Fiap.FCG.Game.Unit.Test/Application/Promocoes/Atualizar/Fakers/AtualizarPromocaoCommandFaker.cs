using AutoBogus;
using Fiap.FCG.Game.Application.Promocoes.Atualizar;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Fakers;

public static class AtualizarPromocaoCommandFaker
{
    public static AtualizarPromocaoCommand Valido()
    {
        return new AutoFaker<AtualizarPromocaoCommand>()
            .RuleFor(c => c.JogosIds, f => f.Make(2, () => f.Random.Int(1, 100)))
            .Generate();
    }
}