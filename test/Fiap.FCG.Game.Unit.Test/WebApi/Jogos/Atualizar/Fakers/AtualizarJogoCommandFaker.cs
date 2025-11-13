using AutoBogus;
using Fiap.FCG.Game.Application.Jogos.Atualizar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Atualizar.Fakers;

public static class AtualizarJogoCommandFaker
{
    public static AtualizarJogoCommand Valido()
    {
        return new AutoFaker<AtualizarJogoCommand>().Generate();
    }
}