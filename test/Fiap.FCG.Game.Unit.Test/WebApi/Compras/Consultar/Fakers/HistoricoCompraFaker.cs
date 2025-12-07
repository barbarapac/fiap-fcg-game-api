using System.Collections.Generic;
using System.Linq;
using Bogus;
using Fiap.FCG.Game.Domain.Compras;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Compras.Consultar.Fakers
{
    public static class HistoricoCompraFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static HistoricoCompra Gerar(int? usuarioId = null)
        {
            var itens = Enumerable.Range(1, Faker.Random.Int(1, 5))
                .Select(_ => ItemCompraFaker.Gerar())
                .ToList();

            return new HistoricoCompra(
                usuarioId ?? Faker.Random.Int(1, 999),
                itens
            );
        }

        public static List<HistoricoCompra> GerarLista(int quantidade, int? usuarioId = null) =>
            Enumerable.Range(1, quantidade)
                .Select(_ => Gerar(usuarioId))
                .ToList();
    }
}