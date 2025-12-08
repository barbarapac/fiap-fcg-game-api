using Bogus;
using Fiap.FCG.Game.Domain.Compras;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fakers
{
    public static class ItemCompraFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static ItemCompra Valido()
        {
            return new ItemCompra(
                Faker.Random.Int(1, 1000),
                Faker.Random.Decimal(50, 500)
            );
        }
    }
}