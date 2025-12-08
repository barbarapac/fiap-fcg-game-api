using Bogus;
using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fakers
{
    public static class ItemCompraFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static ItemCompra Gerar()
        {
            var jogo = new Faker<Jogo>("pt_BR")
                .RuleFor(j => j.Id, f => f.Random.Int(1, 1000))
                .RuleFor(j => j.Nome, f => f.Commerce.ProductName())
                .RuleFor(j => j.Preco, f => f.Random.Decimal(20, 200))
                .Generate();

            var item = new ItemCompra(jogo.Id, jogo.Preco * 0.9m)
            {
                Jogo = jogo
            };

            return item;
        }
    }
}