using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Notificacoes.Consultar;

public interface IUsuarioNotificationGateway
{
    Task<IList<UsuarioNotificavelDto>> ObterUsuariosNotificaveisGrpcAsync(
        CancellationToken cancellationToken = default);
    
    Task<IList<UsuarioNotificavelDto>> ObterUsuariosNotificaveisHttpAsync(
        CancellationToken cancellationToken = default);
}
