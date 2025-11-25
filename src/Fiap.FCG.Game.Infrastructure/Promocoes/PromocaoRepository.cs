using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Promocoes;
using Fiap.FCG.Game.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;

namespace Fiap.FCG.Game.Infrastructure.Promocoes;

public class PromocaoRepository : IPromocaoRepository
{
    private readonly GameDbContext _context;

    public PromocaoRepository(GameDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteAsync(string nome)
    {
        return await _context
            .Set<Promocao>()
            .AnyAsync(p => p.Nome.ToUpper() == nome.ToUpper());
    }

    public async Task<Result<Promocao>> AdicionarAsync(Promocao promocao)
    {
        try
        {
            await _context.Set<Promocao>().AddAsync(promocao);
            await _context.SaveChangesAsync();
            
            return Result.Success(promocao);
        }
        catch (Exception ex)
        {
            return Result.Failure<Promocao>($"Erro ao salvar promoção: {ex.Message}");
        }
    }

    public async Task<List<Promocao>> ObterPromocoesAtivasComJogosAsync()
    {
        var dataAtual = DateTime.UtcNow;

        return await _context.Set<Promocao>()
            .Include(p => p.Jogos)
                .ThenInclude(pj => pj.Jogo)
            .Where(p => p.DataInicio <= dataAtual && p.DataFim >= dataAtual)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<PromocaoJogo>> ObterPorJogosIdsAsync(List<int> jogoIds)
    {
        return await _context.Set<PromocaoJogo>()
            .Where(j => jogoIds.Contains(j.JogoId))
            .Include(x => x.Promocao)
            .Include(x => x.Jogo)
            .ToListAsync();
    }

    public async Task<Promocao> ObterPorIdAsync(int promocaoId)
    {
        return await _context.Set<Promocao>()
            .Include(p => p.Jogos)
                .ThenInclude(pj => pj.Jogo)
            .FirstOrDefaultAsync(x => x.Id == promocaoId);
    }
    
    public async Task<List<Promocao>> ObterTodasAsync()
    {
        return await _context
            .Set<Promocao>()
            .Include(p => p.Jogos)
                .ThenInclude(pj => pj.Jogo)
            .ToListAsync();
    }

    public async Task<Result<string>> ExcluirAsync(Promocao promocao)
    {
        try
        {
            _context.Set<Promocao>().Remove(promocao);
            await _context.SaveChangesAsync();

            return Result.Success("Promoção removida com sucesso");
        }
        catch (Exception ex)
        {
            return Result.Failure<string>($"Erro ao remover promoção: {ex.Message}");
        }
    }
    
    public async Task<Result<string>> AtualizarAsync(Promocao promocao)
    {
        try
        {
            _context.Set<Promocao>().Update(promocao);
            await _context.SaveChangesAsync();

            return Result.Success("Promoção atualizada com sucesso");
        }
        catch (Exception ex)
        {
            return Result.Failure<string>($"Erro ao atualizar promoção: {ex.Message}");
        }
    }
}