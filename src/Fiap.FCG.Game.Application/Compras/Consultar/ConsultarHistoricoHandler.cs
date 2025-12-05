using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Compras;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public class ConsultarHistoricoHandler : IRequestHandler<ConsultarHistoricoQuery, Result<HistoricoCompraResponse>>
    {
        private readonly ICompraRepository _repository;

        public ConsultarHistoricoHandler(ICompraRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<HistoricoCompraResponse>> Handle(ConsultarHistoricoQuery request, CancellationToken cancellationToken)
        {
            var compras = await _repository.ObterPorUsuarioAsync(request.UsuarioId);

            var response = new HistoricoCompraResponse
            {
                Compras = compras.SelectMany(c => c.Itens.Select(i => new HistoricoCompraResponse.CompraDto
                {
                    DataCompra = c.DataCompra,
                    NomeJogo = i.Jogo.Nome,
                    ValorBase = i.Jogo.Preco,
                    ValorComDesconto = i.PrecoPago
                })).ToList()
            };

            return Result.Success(response);
        }
    }
}
