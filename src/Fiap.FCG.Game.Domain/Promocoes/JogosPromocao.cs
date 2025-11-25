using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Domain.Promocoes
{
    public class JogosPromocao
    {
        public int JogoId { get; set; }
        public Jogo Jogo { get; set; }
        public int PromocaoId { get; set; }
        public Promocao Promocao { get; set; }
    }
}
