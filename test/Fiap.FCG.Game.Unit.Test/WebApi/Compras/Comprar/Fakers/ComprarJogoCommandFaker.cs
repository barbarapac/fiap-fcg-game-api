using Bogus;
using Fiap.FCG.Game.Application.Compras.Comprar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Comprar.Fakers
{
    public static class ComprarJogoCommandFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static ComprarJogoCommand GerarValido() =>
            new ComprarJogoCommand()
            {
                UsuarioId = Faker.Random.Int(1, 999),
                JogosIds = Faker.Random.ListItems(new[] { 1, 2, 3, 4, 5 })
            };
    }
}