using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Eventos.PromocaoEvent;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Promocoes;
using MediatR;

namespace Fiap.FCG.Game.Application.Promocoes.Remover;

public class RemoverPromocaoHandler : IRequestHandler<RemoverPromocaoCommand, Result<string>>
{
    private readonly IPromocaoRepository _promocaoRepository;
    private readonly IPromocaoEventPublisher _publisher;

    public RemoverPromocaoHandler(IPromocaoRepository promocaoRepository, IPromocaoEventPublisher publisher)
    {
        _promocaoRepository = promocaoRepository;
        _publisher = publisher;
    }

    public async Task<Result<string>> Handle(RemoverPromocaoCommand request, CancellationToken cancellationToken)
    {
        var promocao = await _promocaoRepository.ObterPorIdAsync(request.PromocaoId);

        await _publisher.PromocaoRemovidaPublishAsync(promocao);
        
        return promocao is null
            ? Result.Failure<string>("A promoção informada não existe")
            : await _promocaoRepository.ExcluirAsync(promocao);
    }
}