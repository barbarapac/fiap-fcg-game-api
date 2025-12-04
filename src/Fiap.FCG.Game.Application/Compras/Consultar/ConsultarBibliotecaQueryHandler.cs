using Fiap.FCG.Game.Domain.Compras;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public class ConsultarBibliotecaQueryHandler : IRequestHandler<ConsultarBibliotecaQuery, List<BibliotecaJogo>>
    {
        private readonly IBibliotecaRepository _repository;

        public ConsultarBibliotecaQueryHandler(IBibliotecaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BibliotecaJogo>> Handle(ConsultarBibliotecaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterBibliotecaAsync(request.UsuarioId);
        }
    }
}
