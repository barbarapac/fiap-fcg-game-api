using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Domain.Compras
{
    public interface IBibliotecaRepository
    {
        Task AdicionarAsync(BibliotecaJogo jogo);
        Task<List<BibliotecaJogo>> ObterPorUsuarioIdAsync(int usuarioId);
        Task<bool> UsuarioJaPossuiJogoAsync(int usuarioId, int jogoId);
    }
}
