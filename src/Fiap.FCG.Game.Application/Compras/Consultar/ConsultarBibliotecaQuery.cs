using Fiap.FCG.Game.Domain._Shared;
using MediatR;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public class ConsultarBibliotecaQuery : IRequest<Result<List<JogoAdquiridoResponse>>>
    {
        public int UsuarioId { get; }

        public ConsultarBibliotecaQuery(int usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
