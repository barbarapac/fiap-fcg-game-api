using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Infrastructure.Usuarios;

public interface IUsuarioNotificationGateway
{
    Task<IList<UsuarioNotificavelDto>> ObterUsuariosNotificaveisAsync(
        CancellationToken cancellationToken = default);
}
