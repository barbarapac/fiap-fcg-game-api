using System.Collections.Generic;
using System.Linq;
using Bogus;
using Fiap.FCG.Game.Domain.Compras;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fakers

{
    public static class BibliotecaJogoFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static BibliotecaJogo Gerar() =>
            new BibliotecaJogo(
                Faker.Random.Int(1, 999),
                Faker.Random.Int(1, 999)
            );

        public static List<BibliotecaJogo> GerarLista(int quantidade) =>
            Faker.Make(quantidade, () => Gerar()).ToList();
    }
}