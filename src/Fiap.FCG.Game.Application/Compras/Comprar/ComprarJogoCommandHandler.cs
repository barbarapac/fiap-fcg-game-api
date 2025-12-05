using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Domain.Promocoes;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application._Shared;

namespace Fiap.FCG.Game.Application.Compras.Comprar
{
    public class ComprarJogoCommandHandler : IRequestHandler<ComprarJogoCommand, Result<bool>>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly ICompraRepository _compraRepository;
        private readonly IBibliotecaRepository _bibliotecaRepository;
        private readonly IPagamentoIntegrationService _pagamentoService;

        public ComprarJogoCommandHandler(
            IJogoRepository jogoRepository,
            IPromocaoRepository promocaoRepository,
            ICompraRepository compraRepository,
            IBibliotecaRepository bibliotecaRepository,
            IPagamentoIntegrationService pagamentoService)
        {
            _jogoRepository = jogoRepository;
            _promocaoRepository = promocaoRepository;
            _compraRepository = compraRepository;
            _bibliotecaRepository = bibliotecaRepository;
            _pagamentoService = pagamentoService;
        }

        public async Task<Result<bool>> Handle(ComprarJogoCommand request, CancellationToken cancellationToken)
        {
            var jogos = await _jogoRepository.ObterPorIdsAsync(request.JogosIds);

            if (jogos.Count != request.JogosIds.Count)
                return Result.Failure<bool>("Um ou mais jogos não foram encontrados.");

            var promocoes = await _promocaoRepository.ObterPorJogosIdsAsync(request.JogosIds);

            var itens = new List<ItemCompra>();

            foreach (var jogo in jogos)
            {
                var jaPossui = await _bibliotecaRepository.UsuarioJaPossuiJogoAsync(request.UsuarioId, jogo.Id);

                if (jaPossui)
                    return Result.Failure<bool>($"O usuário já possui o jogo '{jogo.Nome}'.");

                var promocao = promocoes.FirstOrDefault(p => p.JogoId == jogo.Id);
                decimal valorFinal = promocao != null
                    ? jogo.Preco - (jogo.Preco * promocao.Promocao.DescontoPercentual / 100)
                    : jogo.Preco;

                itens.Add(new ItemCompra(jogo.Id, valorFinal));
            }

            var compra = new HistoricoCompra(request.UsuarioId, itens);
            await _compraRepository.AdicionarAsync(compra);

            foreach (var jogo in jogos)
            {
                await _bibliotecaRepository.AdicionarAsync(new BibliotecaJogo(request.UsuarioId, jogo.Id));
            }

            return Result.Success(true);
        }
    }
}
