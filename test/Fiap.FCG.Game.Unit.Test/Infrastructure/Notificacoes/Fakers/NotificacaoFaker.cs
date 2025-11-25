using Fiap.FCG.Game.Domain.Notificacoes;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Notificacoes.Fakers;

public static class NotificacaoFaker
{
    public static Notificacao Valida()
    {
        var jogo = JogoFaker.Valido();
        var promocao = PromocaoFaker.Valida();
        return Notificacao.Criar(jogo, promocao);
    }
}