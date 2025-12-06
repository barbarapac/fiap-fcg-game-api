using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Infrastructure.Compras
{
    public class CompraRepository : ICompraRepository
    {
        private readonly GameDbContext _context;

        public CompraRepository(GameDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(HistoricoCompra compra)
        {
            await _context.HistoricoCompras.AddAsync(compra);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HistoricoCompra>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _context.HistoricoCompras
                .Include(x => x.Itens)
                .ThenInclude(x => x.Jogo)
                .Where(x => x.UsuarioId == usuarioId)
                .OrderByDescending(x => x.DataCompra)
                .ToListAsync();
        }
    }
}
