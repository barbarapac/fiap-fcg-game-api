using Fiap.FCG.Game.Domain.Compras;
using MediatR;
using System.Collections.Generic;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public record ConsultarBibliotecaQuery(int UsuarioId) : IRequest<List<BibliotecaJogo>>;
}
