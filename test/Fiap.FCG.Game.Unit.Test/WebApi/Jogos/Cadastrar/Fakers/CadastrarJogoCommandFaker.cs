using AutoBogus;
using Fiap.FCG.Game.Application.Jogos.Cadastrar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Cadastrar.Fakers;

public static class CadastrarJogoCommandFaker
{
    public static CadastrarJogoCommand Valido()
    {
        return new AutoFaker<CadastrarJogoCommand>().Generate();
    }
}
