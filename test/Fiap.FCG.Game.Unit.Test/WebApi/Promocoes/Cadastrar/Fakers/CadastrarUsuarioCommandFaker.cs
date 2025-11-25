using AutoBogus;
using Fiap.FCG.Game.Application.Promocoes.Cadastar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Cadastrar.Fakers;

public static class CadastrarUsuarioCommandFaker
{
    public static CadastrarPromocaoCommand Valido()
    {
        return new AutoFaker<CadastrarPromocaoCommand>().Generate();
    }
}