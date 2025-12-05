using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Fiap.FCG.Game.Domain.Promocoes;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes.Fakers
{
    public static class PromocaoFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static Promocao Valida(bool ativa = true)
        {
            var inicio = ativa ? DateTime.UtcNow.AddDays(-2) : DateTime.UtcNow.AddDays(-10);
            var fim = ativa ? DateTime.UtcNow.AddDays(5) : DateTime.UtcNow.AddDays(-1);

            var result = Promocao.Criar(
                Faker.Commerce.ProductName() + Guid.NewGuid(),
                Faker.Commerce.ProductDescription(),
                Faker.Random.Decimal(5, 50),
                inicio,
                fim
            );

            return result.Valor!;
        }

        public static List<Promocao> ListaValida(int quantidade = 3, bool ativas = true)
        {
            return Enumerable.Range(1, quantidade)
                .Select(_ => Valida(ativas))
                .ToList();
        }
    }
}