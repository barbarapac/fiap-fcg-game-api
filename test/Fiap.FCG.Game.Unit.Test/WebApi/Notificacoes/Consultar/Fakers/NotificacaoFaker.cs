using System.Collections.Generic;
using Bogus;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Consultar.Fakers;

public static class NotificacaoFaker
{
    public static List<NotificacaoResponse> ListaValida(int quantidade = 3)
    {
        return new Faker<NotificacaoResponse>()
            .Generate(quantidade);
    }

    public static List<NotificacaoResponse> ListaVazia()
    {
        return new List<NotificacaoResponse>();
    }
}