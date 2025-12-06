using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Domain.Compras
{
    public interface IHistoricoCompraRepository
    {
        Task<List<HistoricoCompra>> ObterHistoricoAsync(int usuarioId);
    }
}
