using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Jogos.Consultar
{
    public interface IConsultaJogoQuery
    {
        Task<List<JogoResponse>> ObterTodosAsync();
        Task<JogoResponse> ObterPorIdAsync(int id);
    }
}
