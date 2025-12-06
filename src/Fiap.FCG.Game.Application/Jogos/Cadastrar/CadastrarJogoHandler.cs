using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Eventos.GameEvent;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Jogos;
using MediatR;

namespace Fiap.FCG.Game.Application.Jogos.Cadastrar
{
    public class CadastrarJogoHandler : IRequestHandler<CadastrarJogoCommand, Result<string>>
    {
        private readonly IJogoRepository _repository;
        private readonly IGameEventPublisher _publisher;

        public CadastrarJogoHandler(IJogoRepository repository, IGameEventPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<Result<string>> Handle(CadastrarJogoCommand request, CancellationToken cancellationToken)
        {
            var existente = await _repository.ObterPorNome(request.Nome);

            if (existente is not null)
                return Result.Failure<string>("Jogo já cadastrado.");

            return await CadastrarAsync(request);
        }

        private async Task<Result<string>> CadastrarAsync(CadastrarJogoCommand request)
        {
            var resultado = Jogo.Criar(request.Nome, request.Preco);
            if (!resultado.Sucesso)
                return Result.Failure<string>(resultado.Erro);

            var adicionarResult = await _repository.AdicionarAsync(resultado.Valor);

            if (!adicionarResult.Sucesso)
            {
                return Result.Failure<string>(adicionarResult.Erro);
            }
            
            await _publisher.JogoCadastradoPublishAsync(adicionarResult.Valor);
            
            return Result.Success(adicionarResult.Valor.Id.ToString());
        }
    }
}
