using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Integracoes
{
    public interface IUsuarioIntegrationService
    {
        Task<bool> UsuarioExiste(int usuarioId);
    }
}
