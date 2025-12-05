using AutoBogus;
using Fiap.FCG.Game.Application.Compras.Comprar;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Fakers
{
    public static class ComprarJogoCommandFaker
    {
        public static ComprarJogoCommand ComValido() =>
        new AutoFaker<ComprarJogoCommand>()
        .RuleFor(x => x.UsuarioId, f => f.Random.Int(1))
        .RuleFor(x => x.JogosIds, f => new List<int> { 1 })
        .Generate();


        public static ComprarJogoCommand ComIdsPersonalizados(List<int> jogosIds) =>
        new AutoFaker<ComprarJogoCommand>()
        .RuleFor(x => x.UsuarioId, f => f.Random.Int(1))
        .RuleFor(x => x.JogosIds, _ => jogosIds)
        .Generate();
    }
}
