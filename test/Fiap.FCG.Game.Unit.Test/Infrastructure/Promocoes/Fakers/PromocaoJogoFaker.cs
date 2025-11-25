using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Domain.Promocoes;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes.Fakers
{
    public static class PromocaoJogoFaker
    {
        public static PromocaoJogo Valido()
        {
            var promocao = PromocaoFaker.Valida();
            var jogo = new Jogo("Jogo Faker", 59.90m);
            var relacao = new PromocaoJogo(jogo.Id, promocao);
            relacao.AdicionarJogo(jogo);
            return relacao;
        }

        public static PromocaoJogo Com(Promocao promocao, Jogo jogo)
        {
            var relacao = new PromocaoJogo(jogo.Id, promocao);
            relacao.AdicionarJogo(jogo);
            return relacao;
        }
    }
}