using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.User.WebApi.Protos;

namespace Fiap.FCG.Game.Infrastructure.Usuarios;

public class UsuarioNotificationGateway : IUsuarioNotificationGateway
{
    private readonly UsuarioService.UsuarioServiceClient _client;

    public UsuarioNotificationGateway(UsuarioService.UsuarioServiceClient client)
    {
        _client = client;
    }

    public async Task<IList<UsuarioNotificavelDto>> ObterUsuariosNotificaveisAsync(
        CancellationToken cancellationToken = default)
    {
        var request = new ObterUsuariosComNotificacoesRequest();

        var response = await _client.ObterUsuariosComNotificacoesAsync(
            request,
            cancellationToken: cancellationToken);

        return response.Usuarios
            .Select(u => new UsuarioNotificavelDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            })
            .ToList();
    }
}