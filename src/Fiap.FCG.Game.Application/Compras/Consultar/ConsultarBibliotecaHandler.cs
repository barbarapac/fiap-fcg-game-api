using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Compras;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Compras.Consultar
{
    public class ConsultarBibliotecaHandler : IRequestHandler<ConsultarBibliotecaQuery, Result<List<JogoAdquiridoResponse>>>
    {
        private readonly IBibliotecaRepository _bibliotecaRepository;

        public ConsultarBibliotecaHandler(IBibliotecaRepository bibliotecaRepository)
        {
            _bibliotecaRepository = bibliotecaRepository;
        }

        public async Task<Result<List<JogoAdquiridoResponse>>> Handle(ConsultarBibliotecaQuery request, CancellationToken cancellationToken)
        {
            var jogos = await _bibliotecaRepository.ObterPorUsuarioIdAsync(request.UsuarioId);

            var resposta = jogos
                .Select(j => new JogoAdquiridoResponse
                {
                    Nome = j.Jogo.Nome
                })
                .ToList();

            return Result.Success(resposta);
        }
    }
}
