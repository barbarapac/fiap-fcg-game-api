using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Infrastructure.Compras
{
    public class BibliotecaRepository : IBibliotecaRepository
    {
        private readonly GameDbContext _context;

        public BibliotecaRepository(GameDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(BibliotecaJogo jogo)
        {
            await _context.BibliotecaJogos.AddAsync(jogo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BibliotecaJogo>> ObterPorUsuarioIdAsync(int usuarioId)
        {
            return await _context.Set<BibliotecaJogo>()
                .Include(b => b.Jogo)
                .Where(b => b.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<bool> UsuarioJaPossuiJogoAsync(int usuarioId, int jogoId)
        {
            return await _context.BibliotecaJogos
                .AnyAsync(x => x.UsuarioId == usuarioId && x.JogoId == jogoId);
        }
    }
}
