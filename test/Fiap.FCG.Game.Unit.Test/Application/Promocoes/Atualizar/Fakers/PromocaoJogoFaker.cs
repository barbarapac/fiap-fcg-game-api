using System.Collections.Generic;
using System.Linq;
using Fiap.FCG.Game.Domain.Promocoes;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Atualizar.Fakers;

public static class PromocaoJogoFaker
{
    public static List<PromocaoJogo> ComConflito(int idPromocaoConflitante, List<int> jogosIds)
    {
        return jogosIds.Select(jogoId =>
            new PromocaoJogo(jogoId, PromocaoFaker.Valida())
        ).Select(pj =>
        {
            typeof(PromocaoJogo).GetProperty(nameof(PromocaoJogo.PromocaoId))!.SetValue(pj, idPromocaoConflitante);
            return pj;
        }).ToList();
    }

    public static List<PromocaoJogo> SemConflito(int idPromocaoAtual, List<int> jogosIds)
    {
        return ComConflito(idPromocaoAtual, jogosIds);
    }
}