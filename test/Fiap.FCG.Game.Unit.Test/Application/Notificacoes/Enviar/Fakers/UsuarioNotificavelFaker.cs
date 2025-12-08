using System.Collections.Generic;
using Bogus;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Fakers;

public static class UsuarioNotificavelFaker
{
    public static List<UsuarioNotificavelDto> ListaValida(int quantidade = 2)
    {
        return new Faker<UsuarioNotificavelDto>()
            .RuleFor(u => u.Id, f => f.Random.Int(1, 9999))
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .Generate(quantidade);
    }

    public static List<UsuarioNotificavelDto> ListaVazia() => [];
}