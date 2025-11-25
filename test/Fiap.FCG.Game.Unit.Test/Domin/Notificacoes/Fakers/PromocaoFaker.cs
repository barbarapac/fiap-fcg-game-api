using AutoBogus;
using Fiap.FCG.Game.Domain.Promocoes;

namespace Fiap.FCG.Game.Unit.Test.Domin.Notificacoes.Fakers;

public static class PromocaoFaker
{
    public static Promocao Valida()
    {
        return new AutoFaker<Promocao>()
            .RuleFor(p => p.Nome, f => f.Commerce.Department())
            .RuleFor(p => p.DescontoPercentual, f => f.Random.Decimal(5, 90))
            .Generate();
    }
}