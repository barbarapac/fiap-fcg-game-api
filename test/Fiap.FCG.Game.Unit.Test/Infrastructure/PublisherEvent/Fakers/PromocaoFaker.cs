using AutoBogus;
using Fiap.FCG.Game.Domain.Promocoes;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.PublisherEvent.Fakers;

public static class PromocaoFaker
{
    public static Promocao Valida()
    {
        var faker = new AutoFaker<Promocao>()
            .RuleFor(p => p.Id, f => f.Random.Int(1, 100))
            .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
            .RuleFor(p => p.Descricao, f => f.Lorem.Sentence())
            .RuleFor(p => p.DescontoPercentual, f => f.Random.Decimal(5, 90))
            .RuleFor(p => p.DataInicio, f => f.Date.Past())
            .RuleFor(p => p.DataFim, (f, p) => f.Date.Future(refDate: p.DataInicio))
            .Ignore(p => p.Jogos);

        var promocao = faker.Generate();
        promocao.AdicionarJogos(new[] { 1, 2, 3 });
        return promocao;
    }
}