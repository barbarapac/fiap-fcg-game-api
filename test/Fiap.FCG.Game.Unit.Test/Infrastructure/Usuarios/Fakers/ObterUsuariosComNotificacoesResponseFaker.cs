using Bogus;
using Fiap.FCG.User.WebApi.Protos;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Usuarios.Fakers;

public static class ObterUsuariosComNotificacoesResponseFaker
{
    public static ObterUsuariosComNotificacoesResponse ComUsuarios(int quantidade = 2)
    {
        var faker = new Faker();

        var response = new ObterUsuariosComNotificacoesResponse();
        for (int i = 0; i < quantidade; i++)
        {
            response.Usuarios.Add(new UsuarioDto
            {
                Id = faker.Random.Int(1, 9999),
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email()
            });
        }

        return response;
    }

    public static ObterUsuariosComNotificacoesResponse Vazio()
    {
        return new ObterUsuariosComNotificacoesResponse(); // Lista de usuários vazia
    }
}