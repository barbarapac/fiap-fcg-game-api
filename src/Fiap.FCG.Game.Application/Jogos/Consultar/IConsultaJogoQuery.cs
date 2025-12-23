using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Jogos.Consultar
{
    public interface IConsultaJogoQuery
    {
        Task<List<JogoResponse>> ObterTodosAsync();
        Task<JogoResponse> ObterPorIdAsync(int id);
        Task<string> ObterPorNomeOrdenadoAsync(string nome, string tipo, bool crescente);
        Task<string> ObterMetricasPrecoAsync();
        Task<string> ObterContagemPorTipoAsync();
        Task<string> ObterJogosMaisCarosOuBaratosAsync(bool maisCaros, int quantidade);
        Task<string> ObterPorTipoEPrecoAsync(string tipo, double precoMin, double precoMax);
    }
}
