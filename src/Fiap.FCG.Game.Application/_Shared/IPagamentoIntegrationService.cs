using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application._Shared
{
    public interface IPagamentoIntegrationService
    {
        Task<bool> ProcessarPagamentoAsync(int usuarioId, int jogoId, decimal valor);
    }
}
