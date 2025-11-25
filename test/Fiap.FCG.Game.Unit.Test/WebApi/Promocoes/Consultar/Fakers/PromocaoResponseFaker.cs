using System.Collections.Generic;
using AutoBogus;
using Fiap.FCG.Game.Application.Promocoes.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Promocoes.Consultar.Fakers;

public static class PromocaoResponseFaker
{
    public static PromocaoResponse Gerar()
    {
        return new AutoFaker<PromocaoResponse>().Generate();
    }

    public static List<PromocaoResponse> GerarLista(int tamanho)
    {
        return new AutoFaker<PromocaoResponse>().Generate(tamanho);
    }
}