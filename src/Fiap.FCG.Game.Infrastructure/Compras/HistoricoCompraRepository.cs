using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Infrastructure.Compras
{
    public class HistoricoCompraRepository : IHistoricoCompraRepository
    {
        private readonly GameDbContext _context;

        public HistoricoCompraRepository(GameDbContext context)
        {
            _context = context;
        }

        public async Task<List<HistoricoCompra>> ObterHistoricoAsync(int usuarioId)
        {
            return await _context.HistoricoCompras
                .Where(x => x.UsuarioId == usuarioId)
                .OrderByDescending(x => x.DataCompra)
                .ToListAsync();
        }
    }
}
