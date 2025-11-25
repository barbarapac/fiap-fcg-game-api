using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiap.FCG.Game.Domain.Notificacoes;
using Fiap.FCG.Game.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;

namespace Fiap.FCG.Game.Infrastructure.Notificacoes;

public class NotificacaoRepository : INotificacaoRepository
{
    private readonly GameDbContext _context;

    public NotificacaoRepository(GameDbContext context)
    {
        _context = context;
    }

    public void Adicionar(Notificacao notificacao)
    {
        _context.Set<Notificacao>().Add(notificacao);
    }

    public async Task<List<int>> ObterUsuariosNaoNotificadosAsync(int promocaoJogoId, List<int> usuarioIds)
    {
        var jaEnviados = await _context.Set<NotificacaoEnviada>()
            .Where(ne => ne.PromocaoJogoId == promocaoJogoId && usuarioIds.Contains(ne.UsuarioId))
            .Select(ne => ne.UsuarioId)
            .ToListAsync();

        return usuarioIds.Except(jaEnviados).ToList();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task<List<NotificacaoEnviada>> ObterTodasAsync()
    {
        return await _context
            .Set<NotificacaoEnviada>()
            .Include(x => x.Notificacao)
            .Include(x => x.PromocaoJogo)
                .ThenInclude(x => x.Promocao)
                .ThenInclude(x => x.Jogos)
                    .ThenInclude(x => x.Jogo)
            .ToListAsync();
    }

    public async Task<List<NotificacaoEnviada>> ObterPorUsuarioAsync(int usuarioId)
    {
        return await _context.Set<NotificacaoEnviada>()
            .Include(x => x.Notificacao)
            .Include(x => x.PromocaoJogo)
                .ThenInclude(x => x.Promocao)
                .ThenInclude(x => x.Jogos)
                    .ThenInclude(x => x.Jogo)
            .Where(x => x.UsuarioId == usuarioId)
            .ToListAsync();
    }
}