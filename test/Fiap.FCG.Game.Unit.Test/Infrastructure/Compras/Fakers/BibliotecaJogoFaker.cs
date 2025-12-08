using System.Collections.Generic;
using System.Linq;
using Bogus;
using Fiap.FCG.Game.Domain.Compras;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fakers
{
    public static class BibliotecaJogoFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static BibliotecaJogo Valido(int? usuarioId = null)
        {
            return new BibliotecaJogo(
                usuarioId ?? Faker.Random.Int(1, 100),
                Faker.Random.Int(1, 300)
            );
        }

        public static List<BibliotecaJogo> ListaValida(int quantidade = 3, int? usuarioId = null)
        {
            return Enumerable.Range(1, quantidade)
                .Select(_ => Valido(usuarioId))
                .ToList();
        }
    }
}