using AutoBogus;
using Fiap.FCG.Game.Application.Promocoes.Atualizar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Atualizar.Fakers;

public static class AtualizarPromocaoCommandFaker
{
    public static AtualizarPromocaoCommand Valido()
        => new AutoFaker<AtualizarPromocaoCommand>().Generate();
}