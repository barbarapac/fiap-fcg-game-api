using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Eventos.GameEvent;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Jogos;
using MediatR;

namespace Fiap.FCG.Game.Application.Jogos.Atualizar
{
    public class AtualizarJogoHandler : IRequestHandler<AtualizarJogoCommand, Result<bool>>
    {
        private readonly IJogoRepository _repository;
        private readonly IGameEventPublisher _publisher;

        public AtualizarJogoHandler(IJogoRepository repository, IGameEventPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<Result<bool>> Handle(AtualizarJogoCommand request, CancellationToken cancellationToken)
        {
            var jogo = await _repository.ObterPorIdAsync(request.Id);
            if (jogo is null)
                return Result.Failure<bool>("Jogo não encontrado.");

            jogo.Atualizar(request.Nome, request.Preco);
            await _repository.AtualizarAsync(jogo);
            await _publisher.JogoEditadoPublishAsync(jogo);

            return Result.Success(true);
        }
    }
}
