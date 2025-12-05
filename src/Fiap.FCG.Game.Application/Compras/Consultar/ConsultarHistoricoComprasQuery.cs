using Fiap.FCG.Game.Domain._Shared;
using MediatR;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public class ConsultarHistoricoComprasQuery : IRequest<Result<HistoricoCompraResponse>>
    {
        public int UsuarioId { get; }

        public ConsultarHistoricoComprasQuery(int usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
