using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Domain.Compras
{
    public interface ICompraRepository
    {
        Task AdicionarAsync(HistoricoCompra compra);
        Task<List<HistoricoCompra>> ObterPorUsuarioAsync(int usuarioId);
    }
}
