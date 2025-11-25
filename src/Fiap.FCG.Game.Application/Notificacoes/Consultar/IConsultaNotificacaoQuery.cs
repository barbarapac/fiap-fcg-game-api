using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Notificacoes.Consultar;

public interface IConsultaNotificacaoQuery
{
    Task<List<NotificacaoResponse>> ObterTodasAsync();
    Task<List<NotificacaoResponse>> ObterPorIdUsuarioAsync(int usuarioId);
}