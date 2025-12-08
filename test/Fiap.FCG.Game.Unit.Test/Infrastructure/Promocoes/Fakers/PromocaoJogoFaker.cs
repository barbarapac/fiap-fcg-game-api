using Bogus;
using Fiap.FCG.Game.Domain.Promocoes;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Jogos.Fakers;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes.Fakers
{
    public static class PromocaoJogoFaker
    {
        private static readonly Faker Faker = new("pt_BR");

        public static PromocaoJogo Valido(Promocao promocao)
        {
            var jogo = JogoFaker.Valido("Jogo Promo");

            var pj = new PromocaoJogo(jogo.Id, promocao);
            pj.AdicionarJogo(jogo);

            return pj;
        }
    }
}