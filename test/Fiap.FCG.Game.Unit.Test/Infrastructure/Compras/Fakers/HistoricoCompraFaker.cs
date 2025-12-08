using Bogus;
using Fiap.FCG.Game.Domain.Compras;
using System.Collections.Generic;
using System.Linq;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fakers
{
    public static class HistoricoCompraFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static HistoricoCompra Valido(int? usuarioId = null)
        {
            return new HistoricoCompra(
                usuarioId ?? Faker.Random.Int(1, 100),
                Enumerable.Range(1, Faker.Random.Int(1, 4))
                    .Select(_ => ItemCompraFaker.Valido())
                    .ToList()
            );
        }

        public static List<HistoricoCompra> ListaValida(int quantidade = 3, int? usuarioId = null)
        {
            return Enumerable.Range(1, quantidade)
                .Select(_ => Valido(usuarioId))
                .ToList();
        }
    }
}