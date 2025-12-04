using Fiap.FCG.Game.Domain.Compras;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public class ConsultarHistoricoComprasHandler : IRequestHandler<ConsultarHistoricoComprasQuery, List<HistoricoCompra>>
    {
        private readonly IHistoricoCompraRepository _repository;

        public ConsultarHistoricoComprasHandler(IHistoricoCompraRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<HistoricoCompra>> Handle(ConsultarHistoricoComprasQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterHistoricoAsync(request.UsuarioId);
        }
    }
}
