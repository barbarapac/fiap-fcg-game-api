using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;
using Fiap.FCG.Game.Domain.Notificacoes;
using Fiap.FCG.Game.Domain.Promocoes;
using MediatR;

namespace Fiap.FCG.Game.Application.Notificacoes.Enviar;

public class EnviarNotificacaoHandler : IRequestHandler<EnviarNotificacaoCommand>
{
    private readonly IPromocaoRepository _promocaoRepository;
    private readonly INotificacaoRepository _notificacaoRepository;
    private readonly IEmailSender _emailSender;
    private readonly IUsuarioNotificationGateway _usuarioNotificationGateway;

    public EnviarNotificacaoHandler(
        IPromocaoRepository promocaoRepository,
        INotificacaoRepository notificacaoRepository,
        IEmailSender emailSender,
        IUsuarioNotificationGateway usuarioNotificationGateway)
    {
        _promocaoRepository = promocaoRepository;
        _notificacaoRepository = notificacaoRepository;
        _emailSender = emailSender;
        _usuarioNotificationGateway = usuarioNotificationGateway;
    }

    public async Task Handle(EnviarNotificacaoCommand request, CancellationToken cancellationToken)
    {
        var usuarios = await _usuarioNotificationGateway.ObterUsuariosNotificaveisHttpAsync();

        if (usuarios.Count == 0) return;
        
        var promocoes = await _promocaoRepository.ObterPromocoesAtivasComJogosAsync();

        if (promocoes.Count != 0)
        {
            await NotificarPromocoesAsync(promocoes, usuarios);
        };
    }

    private async Task NotificarPromocoesAsync(List<Promocao> promocoes, IList<UsuarioNotificavelDto> usuarios)
    { 
        foreach (var promocao in promocoes)
        {
            foreach (var pj in promocao.Jogos)
            {
                var usuariosNaoNotificados = await _notificacaoRepository
                    .ObterUsuariosNaoNotificadosAsync(pj.Id, usuarios.Select(u => u.Id).ToList());

                if (!usuariosNaoNotificados.Any()) continue;

                var notificacao = Notificacao.Criar(pj.Jogo, promocao);

                foreach (var usuarioId in usuariosNaoNotificados)
                {
                    var usuario = usuarios.First(u => u.Id == usuarioId);
                    notificacao.AdicionarEnvio(usuarioId, pj.Id);
                    // await _emailSender.EnviarAsync(usuario.Email, notificacao.Titulo, notificacao.Mensagem);
                }

                _notificacaoRepository.Adicionar(notificacao);
            }
        }

        if (promocoes.Count != 0) 
            await _notificacaoRepository.SaveChangesAsync();
    }
}