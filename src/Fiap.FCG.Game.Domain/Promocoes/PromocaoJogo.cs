using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Domain.Promocoes;

public class PromocaoJogo  : Base
{
    public int PromocaoId { get; set; }
    public int JogoId { get; private set; }
    public Promocao Promocao { get; private set; }
    public Jogo Jogo { get; set; }

    public PromocaoJogo() { }

    public PromocaoJogo(int jogoId, Promocao promocao)
    {
        JogoId = jogoId;
        Promocao = promocao;
    }

    public void AdicionarJogo(Jogo jogo)
    {
        Jogo = jogo;
    }
}
