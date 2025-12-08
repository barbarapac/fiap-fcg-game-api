using Fiap.FCG.Game.Domain._Shared;
using MediatR;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public class ConsultarHistoricoQuery : IRequest<Result<HistoricoCompraResponse>>
    {
        public int UsuarioId { get; }

        public ConsultarHistoricoQuery(int usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
